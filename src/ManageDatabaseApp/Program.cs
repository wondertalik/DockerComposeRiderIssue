using DockerComposeRiderIssue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder.UseConsoleLifetime();

builder.ConfigureServices((host, services) =>
{
    services.AddDbContext<AppDbContext>(options =>
    {
        string? connectionString = host.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("Connections string is null or empty");
        options.UseNpgsql(connectionString, x => x.MigrationsAssembly("BcConfig.Infrastructure.Migrations"));

        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    });
});

IHost host = builder.Build();