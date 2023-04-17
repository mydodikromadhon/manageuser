using CRUD.ManagementUser.Application.Common.Extensions;
using CRUD.ManagementUser.Application.Services.CurrentUser;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CRUD.ManagementUser.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUser;

    public PerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUser)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 1000)
        {
            var requestName = typeof(TRequest).Name;
            var formattedRequest = request.ToPrettyJson();
            var username = _currentUser.Username;
            var ipAddress = _currentUser.IpAddress;

            if (elapsedMilliseconds > 5000)
            {
                _logger.LogError("Processing request for ({ElapsedMilliseconds} milliseconds) by {Username} from {IpAddress}.\n{RequestName}\n{RequestObject}",
                   elapsedMilliseconds, username, ipAddress, requestName, formattedRequest);
            }
            else
            {
                _logger.LogWarning("Processing request for ({ElapsedMilliseconds} milliseconds) by {Username} from {IpAddress}.\n{RequestName}\n{RequestObject}",
                   elapsedMilliseconds, username, ipAddress, requestName, formattedRequest);
            }
        }

        return response;
    }
}
