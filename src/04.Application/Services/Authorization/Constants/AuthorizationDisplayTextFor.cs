using CRUD.ManagementUser.Application.Common.Extensions;

namespace CRUD.ManagementUser.Application.Services.Authorization.Constants;

public static class AuthorizationDisplayTextFor
{
    public static readonly string AuthorizationProvider = nameof(AuthorizationProvider).SplitWords();
    public static readonly string AuthorizationInfo = nameof(AuthorizationInfo).SplitWords();

    public const string Roles = nameof(Roles);
    public const string Role = nameof(Role);
    public const string Permission = nameof(Permission);
    public const string Permissions = nameof(Permissions);
    public const string Key = nameof(Key);
    public const string Value = nameof(Value);
}
