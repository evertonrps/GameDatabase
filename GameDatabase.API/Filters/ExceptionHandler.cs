using System.Net;
using GameDatabase.Domain.Exceptions;
using GameDatabase.Domain.SeedWork;
using Microsoft.AspNetCore.Diagnostics;

namespace GameDatabase.API.Filters;

public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemDetails result;
        switch (exception)
        {
            case ArgumentNullException argumentNullException:
                result = new ProblemDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Type = argumentNullException.GetType().Name,
                    Title = "An unexpected error occurred",
                    Detail = argumentNullException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                };
                logger.LogError(argumentNullException, $"Exception occured : {argumentNullException.Message}");
                break;
            case RecordNotFoundException recordNotFoundException:
                result = new ProblemDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Type = recordNotFoundException.GetType().Name,
                    Title = "Registro não encontrado",
                    Detail = recordNotFoundException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                };
                httpContext.Response.StatusCode = 404;
                logger.LogError(recordNotFoundException, $"Exception occured : {recordNotFoundException.Message}");
                break;
            default:
                result = new ProblemDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Type = exception.GetType().Name,
                    Title = "An unexpected error occurred",
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                };
                logger.LogError(exception, $"Exception occured : {exception.Message}");
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}