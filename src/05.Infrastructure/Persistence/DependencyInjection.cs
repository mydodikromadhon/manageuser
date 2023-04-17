using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.Persistence;
using CRUD.ManagementUser.Infrastructure.Persistence.Common.Constants;
using CRUD.ManagementUser.Application.Common.Extensions;

namespace CRUD.ManagementUser.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var persistenceOptions = configuration.GetSection(PersistenceOptions.SectionKey).Get<PersistenceOptions>();

        var migrationsAssembly = typeof(PersistenceService).Assembly.FullName;

        services.AddDbContext<PersistenceService>(options =>
        {
            options.UseSqlServer(persistenceOptions!.ConnectionString, builder =>
            {
                builder.MigrationsAssembly(migrationsAssembly);
                builder.MigrationsHistoryTable(TableNameFor.EfMigrationsHistory, nameof(CRUD.ManagementUser));
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        services.AddScoped<IPersistenceService>(provider => provider.GetRequiredService<PersistenceService>());

        healthChecksBuilder.AddSqlServer(
            connectionString: persistenceOptions!.ConnectionString,
            name: $"{nameof(PersistenceService).SplitWords()} (SQL Server)");

        return services;
    }
}
