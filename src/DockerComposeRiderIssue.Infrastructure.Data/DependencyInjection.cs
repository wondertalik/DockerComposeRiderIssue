using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DockerComposeRiderIssue.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Connections string is null or empty");
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly("BcConfig.Infrastructure.Migrations"));

            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });
        return services;
    }
}