using Grpc.Core;
using SSO.ApiMessages;
using SSO.Messages;

namespace Endpoint.IntegrationTests;

public class AuthServiceBaseIntegrationTest(TestWebApplicationFactory testWebApplicationFactory)
    : BaseIntegrationTest(testWebApplicationFactory)
{

    [Fact]
    public async Task WhenLoginShouldSuccess()
    {
        //Arrange
        var request = new LoginRequest { AppName = "MyApp", Email = "email@email.com", Password = "qwertyu" };
        var registerRequest = new RegisterRequest{ Email = request.Email, Password = request.Password};
        var registerResponse = await ApiClient.AuthApiClient.RegisterAsync(registerRequest);
        Assert.NotNull(registerResponse);
        
        //Act
        var response = await ApiClient.AuthApiClient.LoginAsync(request);
        
        //Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task WhenRegisterShouldComplete()
    {
        //Arrange
        var request = new RegisterRequest{ Email = "my_email@mail.com", Password = "my_password"};
        
        //Act
        var response = await ApiClient.AuthApiClient.RegisterAsync(request);
        
        //Assert
        Assert.NotNull(response);
    }
    
    [Fact]
    public async Task WhenRegisterAndEmailAlreadyExistShouldThrowException()
    {
        //Arrange
        var request = new RegisterRequest{ Email = "my_email@mail.com", Password = "my_password"};
        var firstResponse = await ApiClient.AuthApiClient.RegisterAsync(request);
        Assert.NotNull(firstResponse);
        
        //Act
        var exception =
            await Assert.ThrowsAsync<RpcException>(async () => await ApiClient.AuthApiClient.RegisterAsync(request));
        
        //Assert
        Assert.Equal(StatusCode.AlreadyExists, exception.StatusCode);
    }
}