using CRUD.ManagementUser.Application.Common.Extensions;
using CRUD.ManagementUser.Application.Services.CurrentUser;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CRUD.ManagementUser.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUser;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var formattedRequest = request.ToPrettyJson();
        var username = _currentUser.Username;
        var ipAddress = _currentUser.IpAddress;

        _logger.LogInformation("Processing request for {Username} from {IpAddress}.\n{RequestName}\n{RequestObject}",
           username, ipAddress, requestName, formattedRequest);

        return Task.CompletedTask;
    }
}
