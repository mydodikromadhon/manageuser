//using System.Security.Claims;
//using CRUD.ManagementUser.Application.Services.Authorization.Constants;

//namespace CRUD.ManagementUser.Bsui.Services.Authentication.Extensions;

//public static class UserInformationReceivedContextExtensions
//{
//    public static async Task LoadAuthorizationClaims(this UserInformationReceivedContext context)
//    {
//        var principal = context.Principal;

//        if (principal is null)
//        {
//            return;
//        }

//        if (principal.Identity is not ClaimsIdentity identity)
//        {
//            return;
//        }

//        var username = context.User.RootElement.GetString(JwtClaimTypes.Email);

//        if (string.IsNullOrWhiteSpace(username))
//        {
//            throw new InvalidOperationException($"User information payload does not contain required JSON element: {JwtClaimTypes.Email}.");
//        }

//        var accessToken = identity.FindFirst(OidcConstants.TokenResponse.AccessToken)!.Value;

//        var serviceProvider = context.HttpContext.RequestServices;
//        var authorizationService = serviceProvider.GetRequiredService<IAuthorizationService>();

//        var authorizationInfo = await authorizationService.GetAuthorizationInfoAsync(username, accessToken);

//        foreach (var role in authorizationInfo.Roles)
//        {
//            if (!identity.Claims.Any(x => x.Type == AuthorizationClaimTypes.Role && x.Value == role.Name))
//            {
//                identity.AddClaim(new Claim(AuthorizationClaimTypes.Role, role.Name));
//            }

//            foreach (var permission in role.Permissions)
//            {
//                if (!identity.Claims.Any(x => x.Type == AuthorizationClaimTypes.Permission && x.Value == permission))
//                {
//                    identity.AddClaim(new Claim(AuthorizationClaimTypes.Permission, permission));
//                }
//            }
//        }
//    }
//}
