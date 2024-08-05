using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using SSO.Messages;
using SSO.Services;

namespace Endpoint.IntegrationTests;

public class AuthServiceBaseIntegrationTest(CustomWebApplicationFactory customWebApplicationFactory)
    : BaseIntegrationTest(customWebApplicationFactory)
{

    [Fact]
    public async Task WhenLoginShouldReturnOne()
    {
        
        var request = new LoginRequest(1, "email", "qwerty");

        //Act
        var response = await ApiClient.AuthApiClient.LoginAsync(request);
        //Assert
        
        Assert.NotNull(response);
        Assert.Equal("test RPC", response.AccessToken);
    }
}