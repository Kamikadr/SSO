using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using SSO.Database;
using Test.Base.Fixtures;

namespace Endpoint.IntegrationTests;

public class SsoFixture
{
    public CustomWebApplicationFactory Factory { get; } = new();

    public async Task ResetDatabaseState()
    {
        using var scope = Factory.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>() ??
                                    throw new InvalidOperationException(
                                        "ApplicationDbContext should be in the services provider");
        
        await GlobalDatabaseInitializer.InitializeIfNeeded(dbContext);

        await using var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync();
            
        var respawner = await Respawner.CreateAsync(connection, CreateRespawnerOptions());
        await respawner.ResetAsync(connection);
    }
    
    private static RespawnerOptions CreateRespawnerOptions()
    {
        return new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToExclude = new[] {"pg_catalog"}
        };
    }
}