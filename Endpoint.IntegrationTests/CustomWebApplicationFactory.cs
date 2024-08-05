using System.Data.Common;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using SSO;
using SSO.Database;
using Testcontainers.PostgreSql;

namespace Endpoint.IntegrationTests;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithDatabase("db_for_testing")
        .WithUsername("user")
        .WithPassword("qwerty")
        .WithPortBinding(5438, 5432)
        .Build();

    private DbConnection? _connection;
    private Respawner? _respawner;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });
            
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory());
        });
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        _connection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
        await _connection.OpenAsync();
        
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        
        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres
        });
    }

    public new Task DisposeAsync()
    {
        _connection?.CloseAsync();
        return Task.CompletedTask;
    }

    public async Task ResetDatabaseAsync()
    {
        if (_connection != null && _respawner != null)
        {
            await _respawner.ResetAsync(_connection);
        }
    }
}