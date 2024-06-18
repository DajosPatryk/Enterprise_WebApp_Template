namespace server.Infrastructure.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;

public class ExceptionHandlingFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionHandlingFilter> _logger;

    public ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception.Message, context.Exception.StackTrace);

        context.Result = context.Exception switch
        {
            NotImplementedException e => new UnprocessableEntityResult(),
            ValidationException e => new ObjectResult(e.Errors) { StatusCode = 400 }, _ => new BadRequestResult()
        };
    }
}
