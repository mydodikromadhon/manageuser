using Microsoft.Extensions.Logging;
using CRUD.ManagementUser.Application.Services.Authorization;
using CRUD.ManagementUser.Application.Services.HealthCheck.Queries.GetHealthCheck;
using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Services.Authorization.Constants;
using CRUD.ManagementUser.Application.Services.Authorization.Models.GetAuthorizationInfo;

namespace CRUD.ManagementUser.Infrastructure.Authorization.None;

public class NoneAuthorizationService : IAuthorizationService
{
    public bool IsUsingAuthorization => false;
    public string ProviderUrl => DefaultTextFor.NA;

    public NoneAuthorizationService(ILogger<NoneAuthorizationService> logger)
    {
        logger.LogWarning("{ServiceName} is set to {ServiceProvider}.", $"{nameof(Authorization)} {CommonDisplayTextFor.Service}", AuthorizationProvider.None);
    }

    public Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetAuthorizationInfoResponse());
    }

    public Task<GetHealthCheckResponse> GetHealthCheckAsync()
    {
        return Task.FromResult(new GetHealthCheckResponse());
    }
}
