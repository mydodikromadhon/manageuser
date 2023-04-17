using CRUD.ManagementUser.Bsui.Services.UI.MudBlazorUI;

namespace CRUD.ManagementUser.Bsui.Services.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUIService(this IServiceCollection services)
    {
        services.AddMudBlazorUIService();

        return services;
    }
}
