using Endpoint.IntegrationTests.Helpers;
using Grpc.Net.Client;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Endpoint.IntegrationTests;

public abstract class BaseIntegrationTest(SsoFixture ssoFixture): IClassFixture<SsoFixture>, IAsyncLifetime
{
    private ApiClientManager? _apiClient;

    protected ApiClientManager ApiClient
    {
        get
        {
            if (_apiClient == null)
            {
                _apiClient = new ApiClientManager(ssoFixture.Factory);
            }

            return _apiClient;
        }
    }

    public Task InitializeAsync()
    {
        _apiClient = new ApiClientManager(ssoFixture.Factory);
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await ssoFixture.ResetDatabaseState();
    }
}