using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.DateAndTime;

namespace CRUD.ManagementUser.Infrastructure.DateAndTime;

public static class DependencyInjection
{
    public static IServiceCollection AddDateAndTimeService(this IServiceCollection services)
    {
        services.AddTransient<IDateAndTimeService, DateAndTimeService>();

        return services;
    }
}
