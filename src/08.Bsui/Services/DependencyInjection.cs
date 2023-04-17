using CRUD.ManagementUser.Bsui.Services.FrontEnd;
using CRUD.ManagementUser.Bsui.Services.Security;
using CRUD.ManagementUser.Bsui.Services.UI;

namespace CRUD.ManagementUser.Bsui.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddBsui(this IServiceCollection services, IConfiguration configuration)
    {
        #region Front End
        services.AddFrontEndService(configuration);
        #endregion Front End

        #region Security
        services.AddSecurityService();
        #endregion Security

        #region User Interface
        services.AddUIService();
        #endregion User Interface

        return services;
    }

    public static IApplicationBuilder UseBsui(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseFrontEndService(configuration);

        return app;
    }
}
