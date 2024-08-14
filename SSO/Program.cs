using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SSO.Database;
using SSO.Services;

namespace SSO;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        AddServices(builder);

        var app = builder.Build();
        
        await SetupDatabase(app);
        ConfigureRouter(app);
        await app.RunAsync();
    }

    

    private static void AddServices(WebApplicationBuilder builder)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Database"));
        dataSourceBuilder.UseNodaTime();
        var dataSource = dataSourceBuilder.Build();
        builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(dataSource, o => o.UseNodaTime()));
        
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviour<,>));
        builder.Services.AddGrpc();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
    
    private static async Task SetupDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
    
    private static void ConfigureRouter(WebApplication app)
    {
        app.MapGrpcService<AuthService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    }
}