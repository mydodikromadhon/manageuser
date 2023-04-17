using CRUD.ManagementUser.Application.Services.UserProfile.Models.GetUserProfile;

namespace CRUD.ManagementUser.Application.Services.UserProfile;

public interface IUserProfileService
{
    Task<GetUserProfileResponse> GetUserProfileAsync(string username, CancellationToken cancellationToken);
}
