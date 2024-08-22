using Grpc.Net.Client;
using SSO.ApiServices;
using SSO.Services;

namespace Endpoint.IntegrationTests;

public class ApiClientManager
{
    public Auth.AuthClient AuthApiClient { get; }

    public ApiClientManager(TestWebApplicationFactory applicationFactory)
    {
        var options = new GrpcChannelOptions { HttpHandler = applicationFactory.Server.CreateHandler() };
        var channel = GrpcChannel.ForAddress(applicationFactory.Server.BaseAddress, options);
        
        AuthApiClient = new Auth.AuthClient(channel);
    }
}