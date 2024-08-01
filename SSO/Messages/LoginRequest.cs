using MediatR;

namespace SSO.Messages;

public partial class LoginRequest : IRequest<LoginResponse>
{
    public LoginRequest(long appId, string email, string password)
    {
        AppId = appId;
        Email = email;
        Password = password;
    }
}

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    public Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new LoginResponse { AccessToken = "test RPC" });
    }
}