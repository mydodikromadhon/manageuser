using CRUD.ManagementUser.Application.Services.HealthCheck.Queries.GetHealthCheck;

namespace CRUD.ManagementUser.Application.Services.Authentication;

public interface IAuthenticationService
{
    bool IsUsingAuthentication { get; }
    string ProviderUrl { get; }
    Task<GetHealthCheckResponse> GetHealthCheckAsync();
}
