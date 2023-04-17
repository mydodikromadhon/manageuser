using CRUD.ManagementUser.Application.Services.HealthCheck.Queries.GetHealthCheck;
using CRUD.ManagementUser.Application.Services.Authorization.Models.GetAuthorizationInfo;

namespace CRUD.ManagementUser.Application.Services.Authorization;

public interface IAuthorizationService
{
    bool IsUsingAuthorization { get; }
    string ProviderUrl { get; }
    Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string username, CancellationToken cancellationToken);
    Task<GetHealthCheckResponse> GetHealthCheckAsync();
}
