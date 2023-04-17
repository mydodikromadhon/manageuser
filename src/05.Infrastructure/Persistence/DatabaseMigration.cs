using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CRUD.ManagementUser.Infrastructure.Persistence;

public static class DatabaseMigration
{
    public static async Task ApplyDatabaseMigrationAsync(this IServiceProvider serviceProvider)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(typeof(DatabaseMigration));
        var persistence = serviceProvider.GetRequiredService<PersistenceService>();
        var isMigrationNeeded = (await persistence.Database.GetPendingMigrationsAsync()).Any();

        if (isMigrationNeeded)
        {
            logger.LogInformation("Applying database migration...");
            persistence.Database.Migrate();
        }
        else
        {
            logger.LogInformation("Database is up to date. No database migration required.");
        }
    }
}
