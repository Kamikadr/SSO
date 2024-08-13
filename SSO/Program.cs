using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSO.Database;
using SSO.Services;

namespace SSO;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        AddServices(builder);

        var app = builder.Build();
        
        ConfigureRouter(app);
        await app.RunAsync();
    }
    
    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviour<,>));
        builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
        builder.Services.AddGrpc();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
    
    private static void ConfigureRouter(WebApplication app)
    {
        app.MapGrpcService<AuthService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    }
}