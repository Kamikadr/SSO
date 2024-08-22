using Grpc.Core;
using Grpc.Core.Interceptors;

namespace SSO.Interceptors;

public class AuthInterceptor: Interceptor
{
    private const string tokenStart = "Bearer ";
    
    public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            var authHeader =  context.RequestHeaders.Get("Autharization")?.Value;

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith(tokenStart))
            {
                throw new RpcException(new Status(StatusCode.Unauthenticated, "No valid JWT token provided"));
            }

            var token = authHeader.Substring(tokenStart.Length).Trim();

            context.UserState["UserContext"] = token;
            await continuation(request, responseStream, context);
        }
        
    }
}