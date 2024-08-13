using FluentValidation;
using Grpc.Core;
using MediatR;

namespace SSO;

public class ValidatorBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failtures = validationResult.SelectMany(result => result.Errors).Where(f => f != null).ToList();

            if (failtures.Count != 0)
            {
                var errorMessage = string.Join(", ", failtures.Select(f => f.ErrorMessage));
                throw new RpcException(new Status(StatusCode.InvalidArgument, errorMessage));
            }
        }

        return await next();
    }
}