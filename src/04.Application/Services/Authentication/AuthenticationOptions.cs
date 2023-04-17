using CRUD.ManagementUser.Application.Services.Authentication.Constants;

namespace CRUD.ManagementUser.Application.Services.Authentication;

public class AuthenticationOptions
{
    public const string SectionKey = nameof(Authentication);

    public string Provider { get; set; } = AuthenticationProvider.None;
}
