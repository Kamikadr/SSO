using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSO.Database;
using SSO.Entities;
using SSO.Extensions;
using SSO.Messages;

namespace SSO.Commands;


public record RegisterCommand(string Email, string Password) : IRequest<RegisterResponse>;


public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be between 6 and more characters long");
    }
}


public class RegisterRequestHandler(ApplicationDbContext dbContext): IRequestHandler<RegisterCommand, RegisterResponse>
{
    public async Task<RegisterResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var newUser = new User(command.Email, command.Password, command.Email);
        try
        {
            await dbContext.Users.AddAsync(newUser, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch(DbUpdateException ex) when(ex.IsUniqueConstraintViolation())
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, "User with the same email already exists"));
        }

        return new RegisterResponse { UserId = newUser.Id };
    }
}