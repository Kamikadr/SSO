using SSO.Messages;

namespace Endpoint.IntegrationTests;

public class AuthServiceBaseIntegrationTest(TestWebApplicationFactory testWebApplicationFactory)
    : BaseIntegrationTest(testWebApplicationFactory)
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

    [Fact]
    public async Task WhenRegisterShouldComplete()
    {
        var request = new RegisterRequest();

        var response = await ApiClient.AuthApiClient.RegisterAsync(request);
        
        Assert.NotNull(response);
    }
}