using Microsoft.Extensions.Logging;
using CRUD.ManagementUser.Application.Services.Authentication;
using CRUD.ManagementUser.Application.Services.HealthCheck.Queries.GetHealthCheck;
using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Services.Authentication.Constants;

namespace CRUD.ManagementUser.Infrastructure.Authentication.None;

public class NoneAuthenticationService : IAuthenticationService
{
    public bool IsUsingAuthentication => false;
    public string ProviderUrl => DefaultTextFor.NA;

    public NoneAuthenticationService(ILogger<NoneAuthenticationService> logger)
    {
        logger.LogWarning("{ServiceName} is set to {ServiceProvider}.", $"{nameof(Authentication)} {CommonDisplayTextFor.Service}", AuthenticationProvider.None);
    }

    public Task<GetHealthCheckResponse> GetHealthCheckAsync()
    {
        throw new NotImplementedException();
    }
}
