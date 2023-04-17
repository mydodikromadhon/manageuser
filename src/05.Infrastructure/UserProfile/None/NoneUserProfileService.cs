using Microsoft.Extensions.Logging;
using CRUD.ManagementUser.Application.Services.UserProfile;
using CRUD.ManagementUser.Application.Services.UserProfile.Models.GetUserProfile;
using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Common.Extensions;
using CRUD.ManagementUser.Application.Services.UserProfile.Constants;

namespace CRUD.ManagementUser.Infrastructure.UserProfile.None;

public class NoneUserProfileService : IUserProfileService
{
    public NoneUserProfileService(ILogger<NoneUserProfileService> logger)
    {
        logger.LogWarning("{ServiceName} is set to {ServiceProvider}.", $"{nameof(UserProfile).SplitWords()} {CommonDisplayTextFor.Service}", UserProfileProvider.None);
    }

    public Task<GetUserProfileResponse> GetUserProfileAsync(string username, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetUserProfileResponse());
    }
}
