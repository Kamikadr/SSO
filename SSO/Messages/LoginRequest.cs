using MediatR;

namespace SSO.Messages;

public partial class LoginRequest: IRequest<LoginResponse>;

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    public Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new LoginResponse { AccessToken = "test RPC" });
    }
}