namespace CRUD.ManagementUser.Infrastructure.HealthCheck;

public class HealthCheckOptions
{
    public const string SectionKey = nameof(HealthCheck);

    public string Endpoint { get; set; } = default!;
}
