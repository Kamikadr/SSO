using FluentValidation;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using SSO.ApiMessages;
using SSO.Database;
using SSO.Entities;
using SSO.Services;

namespace SSO.Messages;

public record LoginQuery(string Email, string Password, string AppName) : IRequest<LoginResponse>;


public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(q => q.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(q => q.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}


public class LoginRequestHandler(ApplicationDbContext dbContext, TokenService tokenService) : IRequestHandler<LoginQuery, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.AuthDatas)
            .ThenInclude(ad => ad.Service).Include(user => user.Services)
            .SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Email or password is invalid."));
        }

        var requestPasswordHash = await HashHelper.GetStringHashAsync(request.Password, user.Salt, cancellationToken);
        if (requestPasswordHash != user.PasswordHash)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Email or password is invalid."));
        }
        
        var tokens = tokenService.GenerateTokens(user);
        var authData = await FindOrCreateAuthData(request, cancellationToken, user);
        authData.RefreshToken = tokens.RefreshToken;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new LoginResponse {AccessToken = tokens.AccessToken, RefreshToken = tokens.RefreshToken};
    }

    private async Task<AuthData> FindOrCreateAuthData(LoginQuery request, CancellationToken cancellationToken, User user)
    {
        var authData = user.AuthDatas.Find(ad => ad.Service.Name == request.AppName);

        if (authData == null)
        {
            var service =
                await dbContext.Services
                    .FirstOrDefaultAsync(s => s.Name == request.AppName
                                              && s.Status == Enums.Status.Active, cancellationToken);
            if (service == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"App with name {request.AppName} not found"));
            }
            
            authData = new AuthData(user.Id, service.Id);
            dbContext.AuthDatas.Add(authData);
        }
        else
        {
            authData.UpdatedAt = SystemClock.Instance.GetCurrentInstant();
        }

        return authData;
    }
}