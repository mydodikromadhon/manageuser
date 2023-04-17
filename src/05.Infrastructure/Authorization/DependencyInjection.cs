using CRUD.ManagementUser.Application.Services.Authorization;
using CRUD.ManagementUser.Infrastructure.Authorization.None;
using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Services.Authentication.Constants;
using CRUD.ManagementUser.Application.Services.Authorization.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.ManagementUser.Infrastructure.Authorization;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<AuthorizationOptions>(configuration.GetSection(AuthorizationOptions.SectionKey));

        var authorizationOptions = configuration.GetSection(AuthorizationOptions.SectionKey).Get<AuthorizationOptions>();

        switch (authorizationOptions!.Provider)
        {
            case AuthorizationProvider.None:
                services.AddNoneAuthorizationService();
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authorization)} {nameof(AuthorizationOptions.Provider)}: {authorizationOptions.Provider}");
        }

        if (authorizationOptions.Provider != AuthorizationProvider.None)
        {
            services.AddAuthorization(config =>
            {
                foreach (var permission in Permissions.All)
                {
                    config.AddPolicy(permission, policy => policy.RequireClaim(AuthorizationClaimTypes.Permission, permission));
                }
            });
        }

        return services;
    }

    public static IApplicationBuilder UseAuthorizationService(this WebApplication app, IConfiguration configuration)
    {
        var authorizationOptions = configuration.GetSection(AuthorizationOptions.SectionKey).Get<AuthorizationOptions>();

        if (authorizationOptions!.Provider != AuthenticationProvider.None)
        {
            app.UseAuthorization();
        }

        return app;
    }
}
