using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.HealthCheck;

namespace CRUD.ManagementUser.Infrastructure.HealthCheck;

public static class DependencyInjection
{
    public static IHealthChecksBuilder AddHealthCheckService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HealthCheckOptions>(configuration.GetSection(HealthCheckOptions.SectionKey));

        services.AddSingleton<IHealthCheckService, HealthCheckService>();

        return services.AddHealthChecks();
    }

    public static IApplicationBuilder UseHealthCheckService(this IApplicationBuilder app, IConfiguration configuration)
    {
        var healthCheckOptions = configuration.GetSection(HealthCheckOptions.SectionKey).Get<HealthCheckOptions>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks(healthCheckOptions!.Endpoint, new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });

        return app;
    }
}
