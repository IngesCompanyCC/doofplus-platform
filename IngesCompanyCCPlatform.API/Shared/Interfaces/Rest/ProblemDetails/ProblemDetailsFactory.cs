using IngesCompanyCCPlatform.API.Shared.Resources;
using IngesCompanyCCPlatform.API.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;

/// <summary>
///     Builds localized problem details responses for the REST controllers of every bounded context.
/// </summary>
public class ProblemDetailsFactory(
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<CommonMessages> commonLocalizer,
    Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory aspNetCoreProblemDetailsFactory)
{
    /// <summary>
    ///     Creates a response whose title is the localized text of the error enum
    ///     (key <c>&lt;Context&gt;Error.&lt;Case&gt;</c> in <c>ErrorMessages</c>).
    /// </summary>
    /// <param name="controller">The controller that answers the request.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="errorEnum">The error of the bounded context, or <c>null</c> for a generic error.</param>
    /// <param name="detailMessage">The localized detail from the application service.</param>
    /// <returns>The problem details result.</returns>
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        Enum? errorEnum,
        string detailMessage)
    {
        var title = errorEnum is not null
            ? errorLocalizer[$"{errorEnum.GetType().Name}.{errorEnum}"].Value
            : commonLocalizer["GenericError"].Value;

        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext, statusCode, title, detail: detailMessage,
            instance: controller.HttpContext.Request.Path);

        return controller.StatusCode(statusCode, problemDetails);
    }

    /// <summary>
    ///     Creates a response from a title key in <c>CommonMessages</c> and a detail key in <c>ErrorMessages</c>.
    /// </summary>
    /// <param name="controller">The controller that answers the request.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="titleKey">The key of the localized title.</param>
    /// <param name="detailKey">The key of the localized detail.</param>
    /// <param name="detailArgs">The values for the placeholders of the detail.</param>
    /// <returns>The problem details result.</returns>
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        string titleKey,
        string detailKey,
        params object[] detailArgs)
    {
        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext, statusCode, commonLocalizer[titleKey],
            detail: errorLocalizer[detailKey, detailArgs],
            instance: controller.HttpContext.Request.Path);

        return controller.StatusCode(statusCode, problemDetails);
    }
}
