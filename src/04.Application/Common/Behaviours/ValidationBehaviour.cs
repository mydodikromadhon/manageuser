using CRUD.ManagementUser.Application.Common.Exceptions;
using CRUD.ManagementUser.Application.Common.Extensions;
using CRUD.ManagementUser.Application.Services.CurrentUser;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CRUD.ManagementUser.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUser, IEnumerable<IValidator<TRequest>> validators)
    {
        _logger = logger;
        _currentUser = currentUser;
        _validators = validators;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(failure => failure is not null).ToList();

            if (failures.Any())
            {
                var requestName = typeof(TRequest).Name;
                var formattedRequest = request.ToPrettyJson();
                var username = _currentUser.Username;
                var ipAddress = _currentUser.IpAddress;
                var exception = new ApplicationValidationException(failures);

                _logger.LogError(exception, "Validation failed when processing request for {Username} from {IpAddress}.\n{RequestName}\n{RequestObject}",
                   username, ipAddress, requestName, formattedRequest);

                throw exception;
            }
        }
    }
}
