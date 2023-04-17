using RestSharp;
using CRUD.ManagementUser.Application.Services.HealthCheck;
using CRUD.ManagementUser.Application.Services.HealthCheck.Queries.GetHealthCheck;
using CRUD.ManagementUser.Application.Services.HealthCheck.Constants;

namespace CRUD.ManagementUser.Infrastructure.HealthCheck;

public class HealthCheckService : IHealthCheckService
{
    private readonly RestClient _restClient;

    public HealthCheckService()
    {
        _restClient = new RestClient("https://localhost:44302");
    }

    public async Task<GetHealthCheckResponse> GetHealthCheckAsync(string healthCheckUrl)
    {
        var restRequest = new RestRequest(healthCheckUrl, Method.Get);
        var restResponse = await _restClient.ExecuteAsync<GetHealthCheckResponse>(restRequest);

        if (restResponse.Data is not null)
        {
            return restResponse.Data;
        }

        if (!restResponse.IsSuccessful)
        {
            return new GetHealthCheckResponse
            {
                Status = HealthCheckStatus.Unhealthy
            };
        }

        return new GetHealthCheckResponse
        {
            Status = HealthCheckStatus.Unknown
        };
    }
}
