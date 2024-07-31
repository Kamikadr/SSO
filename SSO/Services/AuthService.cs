using Grpc.Core;
using MediatR;
using SSO.Messages;

namespace SSO.Services;

public class AuthService(IMediator mediator): Auth.AuthBase
{
    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(request, context.CancellationToken);
        return result;
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(request, context.CancellationToken);
        return result;
    }
}