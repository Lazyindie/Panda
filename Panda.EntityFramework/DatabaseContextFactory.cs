using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Panda.EntityFramework;

namespace Panda.Infrastructure;

internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    /// <summary>
    /// Creates the DB context.
    /// </summary>
    public DatabaseContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DatabaseContext");
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

        // For this test, using sql server
        optionsBuilder.UseSqlServer(
            connectionString,
            options => options
                .MigrationsHistoryTable("MigrationsHistory", "EntityFramework")
                .EnableRetryOnFailure());

        // Example of other uses.
        // options.UseSqlite(connectionString); // Example for SQLite
        // optionsBuilder.UseNpgsql(connectionString); // Example for PostgreSQL
        // optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); // Example for MySQL / MariaDB

        return new DatabaseContext(optionsBuilder.Options);
    }
}
