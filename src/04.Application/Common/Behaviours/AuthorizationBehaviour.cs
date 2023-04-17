using System.Reflection;
using MediatR;
using CRUD.ManagementUser.Application.Common.Attributes;
using CRUD.ManagementUser.Application.Common.Exceptions;
using CRUD.ManagementUser.Application.Services.Authentication;
using CRUD.ManagementUser.Application.Services.Authorization;
using CRUD.ManagementUser.Application.Services.CurrentUser;
using CRUD.ManagementUser.Application.Services.Authorization.Constants;

namespace CRUD.ManagementUser.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private const string StartingErrorMessage = "The server is refusing to process the request because the user";
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IAuthenticationService authenticationService, IAuthorizationService authorizationService)
    {
        _currentUserService = currentUserService;
        _authenticationService = authenticationService;
        _authorizationService = authorizationService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (!authorizeAttributes.Any())
        {
            return await next();
        }

        if (!_authenticationService.IsUsingAuthentication)
        {
            return await next();
        }

        if (!_currentUserService.UserId.HasValue)
        {
            throw new UnauthorizedAccessException($"{StartingErrorMessage} is not authenticated.");
        }

        if (!_authorizationService.IsUsingAuthorization)
        {
            return await next();
        }

        var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));

        if (!authorizeAttributesWithPolicies.Any())
        {
            return await next();
        }

        var authorizationInfo = await _authorizationService.GetAuthorizationInfoAsync(_currentUserService.Username, cancellationToken);

        foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
        {
            var authorized = authorizationInfo.Roles.SelectMany(x => x.Permissions).Any(x => x.Equals(policy, StringComparison.OrdinalIgnoreCase));

            if (!authorized)
            {
                throw new ForbiddenAccessException($"{StartingErrorMessage} {_currentUserService.Username} does not have the following {AuthorizationClaimTypes.Permission}: {policy}");
            }
        }

        return await next();
    }
}
