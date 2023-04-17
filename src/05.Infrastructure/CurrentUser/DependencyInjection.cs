using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.CurrentUser;

namespace CRUD.ManagementUser.Infrastructure.CurrentUser;

public static class DependencyInjection
{
    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
