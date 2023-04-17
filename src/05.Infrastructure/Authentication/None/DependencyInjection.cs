using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.Authentication;

namespace CRUD.ManagementUser.Infrastructure.Authentication.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneAuthenticationService(this IServiceCollection services)
    {
        services.AddTransient<IAuthenticationService, NoneAuthenticationService>();

        return services;
    }
}
