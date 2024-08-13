using AutoMapper;
using Grpc.Core;
using MediatR;
using SSO.Commands;
using SSO.Messages;

namespace SSO.Services;

public class AuthService(IMediator mediator, IMapper mapper): Auth.AuthBase
{
    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var query = mapper.Map<LoginQuery>(request);
        var result = await mediator.Send(query, context.CancellationToken);
        return result;
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        var command = mapper.Map<RegisterCommand>(request);
        var result = await mediator.Send(command, context.CancellationToken);
        return result;
    }
}