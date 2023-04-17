using CRUD.ManagementUser.Application.Common.Extensions;
using CRUD.ManagementUser.Application.Services.CurrentUser;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CRUD.ManagementUser.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUser;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            var requestName = typeof(TRequest).Name;
            var formattedRequest = request.ToPrettyJson();
            var username = _currentUser.Username;
            var ipAddress = _currentUser.IpAddress;

            _logger.LogError(exception, "Unhandled Exception when processing request for {Username} from {IpAddress}.\n{RequestName}\n{RequestObject}",
               username, ipAddress, requestName, formattedRequest);

            throw;
        }
    }
}
