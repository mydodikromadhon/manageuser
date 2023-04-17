using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.Authorization;

namespace CRUD.ManagementUser.Infrastructure.Authorization.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneAuthorizationService(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationService, NoneAuthorizationService>();

        return services;
    }
}
