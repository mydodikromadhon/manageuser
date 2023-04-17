namespace CRUD.ManagementUser.Infrastructure.HealthCheckStorage;

public class HealthCheckStorageOptions
{
    public const string SectionKey = nameof(HealthCheckStorage);

    public string ConnectionString { get; set; } = default!;
    public Endpoints Endpoints { get; set; } = default!;
}

public class Endpoints
{
    public string UI { get; set; } = default!;
    public string Api { get; set; } = default!;
}
