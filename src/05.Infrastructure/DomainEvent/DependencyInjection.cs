using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.DomainEvent;

namespace CRUD.ManagementUser.Infrastructure.DomainEvent;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainEventService(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();

        return services;
    }
}
