using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.ManagementUser.Infrastructure.Persistence;

public static class DatabaseSeeding
{
    public static async Task ApplyDatabaseSeedingAsync(this IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var persistenceOptions = configuration.GetSection(PersistenceOptions.SectionKey).Get<PersistenceOptions>();

        if (persistenceOptions!.IsSeedingEnabled)
        {
            await Task.CompletedTask;
        }
    }
}
