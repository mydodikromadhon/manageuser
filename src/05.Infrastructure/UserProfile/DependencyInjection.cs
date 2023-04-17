using CRUD.ManagementUser.Infrastructure.UserProfile.None;
using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Common.Extensions;
using CRUD.ManagementUser.Application.Services.UserProfile.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD.ManagementUser.Infrastructure.UserProfile;

public static class DependencyInjection
{
    public static IServiceCollection AddUserProfileService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var userProfileOptions = configuration.GetSection(UserProfileOptions.SectionKey).Get<UserProfileOptions>();

        switch (userProfileOptions!.Provider)
        {
            case UserProfileProvider.None:
                services.AddNoneUserProfileService();
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(UserProfile).SplitWords()} {nameof(UserProfileOptions.Provider)}: {userProfileOptions.Provider}");
        }

        return services;
    }
}
