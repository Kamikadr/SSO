namespace Endpoint.IntegrationTests;

public abstract class BaseIntegrationTest(TestWebApplicationFactory testWebApplicationFactory)
    : IClassFixture<TestWebApplicationFactory>, IAsyncLifetime
{
    private ApiClientManager? _apiClient;

    protected ApiClientManager ApiClient
    {
        get
        {
            if (_apiClient == null)
            {
                _apiClient = new ApiClientManager(testWebApplicationFactory);
            }

            return _apiClient;
        }
    }

    public Task InitializeAsync()
    {
        _apiClient = new ApiClientManager(testWebApplicationFactory);
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await testWebApplicationFactory.ResetDatabaseAsync();
    }
}