using CRUD.ManagementUser.Application.Services.UserProfile.Constants;

namespace CRUD.ManagementUser.Infrastructure.UserProfile;

public class UserProfileOptions
{
    public const string SectionKey = nameof(UserProfile);

    public string Provider { get; set; } = UserProfileProvider.None;
}

