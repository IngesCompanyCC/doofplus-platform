using System.Net.Mime;
using System.Text.Json;
using IngesCompanyCCPlatform.API.Shared.Resources;
using IngesCompanyCCPlatform.API.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Middleware version of <see cref="GlobalExceptionHandler" />, kept as in learning-center-platform.
/// </summary>
[Obsolete("Use ASP.NET Core IExceptionHandler with GlobalExceptionHandler and app.UseExceptionHandler() instead.")]
public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<CommonMessages> commonLocalizer)
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    /// <summary>
    ///     Runs the next component and writes a problem details response if it throws.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "Request was cancelled: {Message}", ex.Message);
            await WriteAsync(context, StatusCodes.Status409Conflict,
                errorLocalizer["OperationCancelled"], errorLocalizer["OperationCancelled"]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await WriteAsync(context, StatusCodes.Status500InternalServerError,
                commonLocalizer["InternalServerError"], errorLocalizer["GenericError"]);
        }
    }

    private static async Task WriteAsync(HttpContext context, int status, string title, string detail)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = status;
        var problemDetails = new ProblemDetails { Status = status, Title = title, Detail = detail, Instance = context.Request.Path };
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, JsonOptions));
    }
}
