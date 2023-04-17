using CRUD.ManagementUser.Domain.Entities;
using CRUD.ManagementUser.Infrastructure.AppInfo;
using CRUD.ManagementUser.Infrastructure.Authentication;
using CRUD.ManagementUser.Infrastructure.Authorization;
using CRUD.ManagementUser.Infrastructure.CurrentUser;
using CRUD.ManagementUser.Infrastructure.DateAndTime;
using CRUD.ManagementUser.Infrastructure.DomainEvent;
using CRUD.ManagementUser.Infrastructure.HealthCheck;
using CRUD.ManagementUser.Infrastructure.HealthCheckStorage;
using CRUD.ManagementUser.Infrastructure.Persistence;
using CRUD.ManagementUser.Infrastructure.UserProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.ManagementUser.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Health Check
        var healthChecksBuilder = services.AddHealthCheckService(configuration);
        #endregion Health Check

        //#region Health Check Storage
        //services.AddHealthCheckStorageService(configuration);
        //#endregion Health Check Storage

        #region AppInfo
        services.AddAppInfoService(configuration);
        #endregion AppInfo

        #region Authentication
        services.AddAuthenticationService(configuration, healthChecksBuilder);
        #endregion Authentication

        #region Authorization
        services.AddAuthorizationService(configuration, healthChecksBuilder);
        #endregion Authorization

        #region Current User
        services.AddCurrentUserService();
        #endregion Current User

        #region DateTime
        services.AddDateAndTimeService();
        #endregion DateTime

        #region Domain Event
        services.AddDomainEventService();
        #endregion Domain Event
        
        #region Persistence
        services.AddPersistenceService(configuration, healthChecksBuilder);
        #endregion Persistence

        #region User Profile
        services.AddUserProfileService(configuration, healthChecksBuilder);
        #endregion User Profile

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<PersistenceService>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this WebApplication app, IConfiguration configuration)
    {
        #region Authentication
        app.UseAuthenticationService(configuration);
        #endregion Authentication

        #region Authorization
        app.UseAuthorizationService(configuration);
        #endregion Authorization

        #region Health Check
        app.UseHealthCheckService(configuration);
        #endregion Health Check

        #region Health Check Storage
        app.UseHealthCheckStorageService(configuration);
        #endregion Health Check Storage

        return app;
    }
}
