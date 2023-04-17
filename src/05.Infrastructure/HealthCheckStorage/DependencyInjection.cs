using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.HealthCheckStorage;
using CRUD.ManagementUser.Infrastructure.HealthCheck;

namespace CRUD.ManagementUser.Infrastructure.HealthCheckStorage;

public static class DependencyInjection
{
    public static IServiceCollection AddHealthCheckStorageService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HealthCheckStorageOptions>(configuration.GetSection(HealthCheckStorageOptions.SectionKey));

        var healthCheckOptions = configuration.GetSection(HealthCheckOptions.SectionKey).Get<HealthCheckOptions>();
        var healthChecksUIBuilder = services.AddHealthChecksUI(settings => settings.AddHealthCheckEndpoint($"{nameof(Infrastructure)} {nameof(Application.Services)}", healthCheckOptions!.Endpoint));
        var healthCheckStorageOptions = configuration.GetSection(HealthCheckStorageOptions.SectionKey).Get<HealthCheckStorageOptions>();

        healthChecksUIBuilder.Services.AddTransient<IHealthCheckStorageService, HealthCheckStorageService>();

        healthChecksUIBuilder.AddSqlServerStorage(healthCheckStorageOptions!.ConnectionString, options =>
        {
            options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            options.ConfigureWarnings(wcb => wcb.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        return services;
    }

    public static IApplicationBuilder UseHealthCheckStorageService(this IApplicationBuilder app, IConfiguration configuration)
    {
        var healthCheckStorageOptions = configuration.GetSection(HealthCheckStorageOptions.SectionKey).Get<HealthCheckStorageOptions>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecksUI(options =>
            {
                options.UIPath = healthCheckStorageOptions!.Endpoints.UI;
                options.ApiPath = healthCheckStorageOptions.Endpoints.Api;
                options.AddCustomStylesheet(@"wwwroot\healthchecks\site.css");
            });
        });

        return app;
    }
}
