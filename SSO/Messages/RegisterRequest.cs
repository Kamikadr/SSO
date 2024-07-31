using MediatR;

namespace SSO.Messages;

public partial class RegisterRequest : IRequest<RegisterResponse>;

public class RegisterRequestHandler: IRequestHandler<RegisterRequest, RegisterResponse>
{
    public Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new RegisterResponse { UserId = 1 });
    }
}