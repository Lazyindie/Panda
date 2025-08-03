using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Panda.EntityFramework;

namespace Panda.Api.Configurations;

public static class EntityFrameworkConfiguration
{
    public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(
            builder => {
                var connectionString = configuration.GetConnectionString("DatabaseContext");

                builder.UseSqlServer(
                    connectionString,
                    options => options
                        .MigrationsHistoryTable("MigrationsHistory", "EntityFramework")
                        .EnableRetryOnFailure());
            });

        services.AddScoped<IDatabaseContext>(serviceProvider => serviceProvider.GetService<DatabaseContext>()!);

        return services;
    }
}
