namespace server.Application.Behaviors;

using FluentValidation;
using FluentValidation.Results;
using MediatR;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

    public ValidatorBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var typeName = request.GetType().Name;

        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(r => r.Errors)
            .Where(e => e != null)
            .Select(x => new ValidationFailure()
            {
                ErrorMessage = x.ErrorMessage,
                ErrorCode = x.ErrorCode,
                PropertyName = x.PropertyName
            })
            .ToList();

        if (failures.Any()) throw new ValidationException("Validation exception", failures);

        return await next();
    }
}
