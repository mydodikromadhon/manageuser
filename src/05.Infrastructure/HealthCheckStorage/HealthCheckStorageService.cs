using CRUD.ManagementUser.Application.Services.HealthCheckStorage;

namespace CRUD.ManagementUser.Infrastructure.HealthCheckStorage;

public class HealthCheckStorageService : IHealthCheckStorageService
{
    public bool Enabled => true;
}
