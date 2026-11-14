using IngesCompanyCCPlatform.API.Shared.Infrastructure.Localization;
using IngesCompanyCCPlatform.API.Shared.Resources;
using IngesCompanyCCPlatform.API.Shared.Resources.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Turns the unhandled exceptions of a request into a localized problem details response.
/// </summary>
public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<CommonMessages> commonLocalizer,
    IStringLocalizerFactory localizerFactory,
    IEnumerable<BoundedContextMessages> boundedContextMessages)
    : IExceptionHandler
{
    /// <summary>
    ///     Writes a 409 response for a cancelled operation, a 400 response for an invalid argument
    ///     and a 500 response for any other exception.
    /// </summary>
    /// <param name="httpContext">The HTTP context of the current request.</param>
    /// <param name="exception">The unhandled exception.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> when the problem details response was written.</returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException operationCanceled)
        {
            logger.LogWarning(operationCanceled, "Request was cancelled: {Message}", operationCanceled.Message);
            return await WriteAsync(httpContext, exception, new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = errorLocalizer["OperationCancelled"],
                Detail = errorLocalizer["OperationCancelled"],
                Instance = httpContext.Request.Path
            });
        }

        if (exception is ArgumentException argumentException)
        {
            logger.LogWarning(argumentException, "Request has an invalid argument: {Message}", argumentException.Message);
            return await WriteAsync(httpContext, exception, new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = errorLocalizer["InvalidArgument"],
                Detail = LocalizeArgumentMessage(argumentException),
                Instance = httpContext.Request.Path
            });
        }

        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        return await WriteAsync(httpContext, exception, new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = commonLocalizer["InternalServerError"],
            Detail = errorLocalizer["GenericError"],
            Instance = httpContext.Request.Path
        });
    }

    /// <summary>
    ///     Translates the message of the exception when it is a key of a registered bounded context;
    ///     otherwise, returns the message as it is.
    /// </summary>
    private string LocalizeArgumentMessage(ArgumentException exception)
    {
        var message = exception.Message;
        if (exception.ParamName is not null)
        {
            // ArgumentException appends " (Parameter 'name')" to the message; the key is what comes before.
            var suffix = message.LastIndexOf($" (Parameter '{exception.ParamName}')", StringComparison.Ordinal);
            if (suffix >= 0) message = message[..suffix];
        }

        foreach (var messages in boundedContextMessages)
        {
            var localized = localizerFactory.Create(messages.MessagesType)[message];
            if (!localized.ResourceNotFound) return localized.Value;
        }

        return message;
    }

    private async ValueTask<bool> WriteAsync(HttpContext httpContext, Exception exception, ProblemDetails problemDetails)
    {
        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        });
    }
}
