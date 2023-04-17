using CRUD.ManagementUser.Application.Services.HealthCheck.Queries.GetHealthCheck;

namespace CRUD.ManagementUser.Application.Services.HealthCheck;

public interface IHealthCheckService
{
    Task<GetHealthCheckResponse> GetHealthCheckAsync(string healthCheckUrl);
}
