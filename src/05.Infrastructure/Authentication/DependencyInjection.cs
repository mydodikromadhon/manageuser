using CRUD.ManagementUser.Application.Services.Authentication;
using CRUD.ManagementUser.Infrastructure.Authentication.None;
using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Services.Authentication.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.ManagementUser.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.SectionKey));

        var authenticationOptions = configuration.GetSection(AuthenticationOptions.SectionKey).Get<AuthenticationOptions>();

        switch (authenticationOptions!.Provider)
        {
            case AuthenticationProvider.None:
                services.AddNoneAuthenticationService();
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authentication)} {nameof(AuthenticationOptions.Provider)}: {authenticationOptions.Provider}");
        }

        return services;
    }

    public static IApplicationBuilder UseAuthenticationService(this IApplicationBuilder app, IConfiguration configuration)
    {
        var authenticationOptions = configuration.GetSection(AuthenticationOptions.SectionKey).Get<AuthenticationOptions>();

        switch (authenticationOptions!.Provider)
        {
            case AuthenticationProvider.None:
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authorization)} {nameof(AuthenticationOptions.Provider)}: {authenticationOptions.Provider}");
        }

        return app;
    }
}
