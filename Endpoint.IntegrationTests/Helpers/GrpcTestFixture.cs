namespace Endpoint.IntegrationTests.Helpers;

public class GrpcTestFixture<TStartup>: IDisposable where TStartup: class
{
    public GrpcTestFixture()
    {
        
    }
    
    
    public void Dispose()
    {
        // TODO release managed resources here
    }
}