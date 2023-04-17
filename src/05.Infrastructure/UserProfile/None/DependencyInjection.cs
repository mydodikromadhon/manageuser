﻿using Microsoft.Extensions.DependencyInjection;
using CRUD.ManagementUser.Application.Services.UserProfile;

namespace CRUD.ManagementUser.Infrastructure.UserProfile.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneUserProfileService(this IServiceCollection services)
    {
        services.AddTransient<IUserProfileService, NoneUserProfileService>();

        return services;
    }
}
