using CRUD.ManagementUser.Application.Services.HealthCheck.Constants;

namespace CRUD.ManagementUser.Application.Services.HealthCheck.Queries.GetHealthCheck;

public class GetHealthCheckResponse
{
    public string Status { get; set; } = HealthCheckStatus.Loading;
    public TimeSpan TotalDuration { get; set; }
    public IDictionary<string, GetHealthCheckHealthCheckEntry> Entries { get; set; } = new Dictionary<string, GetHealthCheckHealthCheckEntry>();
}
