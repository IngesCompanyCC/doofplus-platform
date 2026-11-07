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
    IStringLocalizer<CommonMessages> commonLocalizer)
    : IExceptionHandler
{
    /// <summary>
    ///     Writes a 409 response for a cancelled operation and a 500 response for any other exception.
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

        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        return await WriteAsync(httpContext, exception, new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = commonLocalizer["InternalServerError"],
            Detail = errorLocalizer["GenericError"],
            Instance = httpContext.Request.Path
        });
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
