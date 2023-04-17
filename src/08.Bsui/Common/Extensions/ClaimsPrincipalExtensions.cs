using System.Security.Claims;
using IdentityModel;
using CRUD.ManagementUser.Application.Services.Authentication.Constants;
using CRUD.ManagementUser.Application.Services.Authorization.Constants;

namespace CRUD.ManagementUser.Bsui.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(x => x.Type == JwtClaimTypes.Subject)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return null;
        }

        return new Guid(userId);
    }

    public static string? GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(x => x.Type == JwtClaimTypes.Email)?.Value;
    }

    public static DateTimeOffset? GetAuthenticationTime(this ClaimsPrincipal user)
    {
        var authenticationTime = user.FindFirst(x => x.Type == JwtClaimTypes.AuthenticationTime)?.Value;

        if (string.IsNullOrWhiteSpace(authenticationTime))
        {
            return null;
        }

        return DateTimeOffset.FromUnixTimeSeconds(long.Parse(authenticationTime));
    }

    public static string? GetAccessToken(this ClaimsPrincipal user)
    {
        return user.FindFirst(x => x.Type == OidcConstants.TokenResponse.AccessToken)?.Value;
    }

    public static string? GetRefreshToken(this ClaimsPrincipal user)
    {
        return user.FindFirst(x => x.Type == OidcConstants.TokenResponse.RefreshToken)?.Value;
    }

    public static string? GetDisplayName(this ClaimsPrincipal user)
    {
        return user.FindFirst(x => x.Type == CustomClaimTypes.DisplayName)?.Value;
    }

    public static string? GetEmployeeId(this ClaimsPrincipal user)
    {
        return user.FindFirst(x => x.Type == CustomClaimTypes.EmployeeId)?.Value;
    }

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal user)
    {
        return user.Claims.Where(x => x.Type == AuthorizationClaimTypes.Role).Select(x => x.Value);
    }

    public static bool IsInRole(this ClaimsPrincipal user, string role)
    {
        return user.Claims.Any(x => x.Type == AuthorizationClaimTypes.Role && x.Value == role);
    }

    public static IEnumerable<string> GetPermissions(this ClaimsPrincipal user)
    {
        return user.Claims.Where(x => x.Type == AuthorizationClaimTypes.Permission).Select(x => x.Value);
    }

    public static bool HasPermission(this ClaimsPrincipal user, string permission)
    {
        return user.Claims.Any(x => x.Type == AuthorizationClaimTypes.Permission && x.Value == permission);
    }
}
