using CRUD.ManagementUser.Application.Services.Authorization.Constants;

namespace CRUD.ManagementUser.Application.Services.Authorization;

public class AuthorizationOptions
{
    public const string SectionKey = nameof(Authorization);

    public string Provider { get; set; } = AuthorizationProvider.None;
}
