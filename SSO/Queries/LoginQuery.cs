using FluentValidation;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSO.Database;
using SSO.Services;

namespace SSO.Messages;

public record LoginQuery(string Email, string Password) : IRequest<LoginResponse>;


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
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
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
        return new LoginResponse {AccessToken = tokens.AccessToken, RefreshToken = tokens.RefreshToken};
    }
}