using Endpoint.IntegrationTests.Helpers;
using Grpc.Net.Client;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Endpoint.IntegrationTests;

public abstract class BaseIntegrationTest(CustomWebApplicationFactory customWebApplicationFactory): IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private ApiClientManager? _apiClient;

    protected ApiClientManager ApiClient
    {
        get
        {
            if (_apiClient == null)
            {
                _apiClient = new ApiClientManager(customWebApplicationFactory);
            }

            return _apiClient;
        }
    }

    public Task InitializeAsync()
    {
        _apiClient = new ApiClientManager(customWebApplicationFactory);
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await customWebApplicationFactory.ResetDatabaseAsync();
    }
}