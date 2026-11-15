using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Resources;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts the results of the Quality &amp; Compliance services into HTTP responses.
/// </summary>
/// <remarks>
///     The title of each problem details response comes from <c>ErrorMessages</c> (<c>QualityError.&lt;Case&gt;</c>)
///     and the detail from <see cref="QualityMessages" />, whose keys are the codes of the domain errors. Invalid data
///     (the keys thrown by the aggregates) is answered with a 400 by the global exception handler of Shared.
/// </remarks>
public static class QualityActionResultAssembler
{
    private static int ToStatusCodeFromQualityError(QualityError error)
    {
        return error switch
        {
            QualityError.QualityDocumentNotFound => StatusCodes.Status404NotFound,
            QualityError.DeviationNotFound => StatusCodes.Status404NotFound,
            QualityError.DuplicateDeviationCode => StatusCodes.Status409Conflict,
            QualityError.EvidenceNotFound => StatusCodes.Status404NotFound,
            QualityError.CapaPlanNotFound => StatusCodes.Status404NotFound,
            QualityError.DuplicateCapaPlanCode => StatusCodes.Status409Conflict,
            QualityError.CapaActionNotFound => StatusCodes.Status404NotFound,
            QualityError.AnalyticalResultNotFound => StatusCodes.Status404NotFound,
            QualityError.AuditNotFound => StatusCodes.Status404NotFound,
            QualityError.DuplicateAuditCode => StatusCodes.Status409Conflict,
            QualityError.AuditFindingNotFound => StatusCodes.Status404NotFound,
            QualityError.AuditEventNotFound => StatusCodes.Status404NotFound,
            QualityError.RegulatoryReportNotFound => StatusCodes.Status404NotFound,
            QualityError.DuplicateRegulatoryReportCode => StatusCodes.Status409Conflict,
            QualityError.CollaborationTaskNotFound => StatusCodes.Status404NotFound,
            QualityError.TaskCommentNotFound => StatusCodes.Status404NotFound,
            QualityError.TraceabilityGapNotFound => StatusCodes.Status404NotFound,
            QualityError.EvidenceReviewed => StatusCodes.Status409Conflict,
            QualityError.BatchReleaseNotFound => StatusCodes.Status404NotFound,
            QualityError.BatchReleaseAlreadyRequested => StatusCodes.Status409Conflict,
            QualityError.BatchReleaseStateConflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }

    /// <summary>
    ///     Gets the localized text of a message key, or the fallback when the key has no translation.
    /// </summary>
    public static string Localize(IStringLocalizer<QualityMessages> messages, string key, string fallback,
        params object[] args)
    {
        try
        {
            var message = messages[key, args];
            return message.ResourceNotFound ? fallback : message.Value;
        }
        catch (FormatException)
        {
            // The message needs a value (for example the id) that the caller did not pass.
            return fallback;
        }
    }

    /// <summary>
    ///     Returns the success response, or the problem details of the failed command.
    /// </summary>
    public static IActionResult ToActionResultFromCommandResult<T>(
        ControllerBase controller,
        Result<T> result,
        IStringLocalizer<QualityMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction,
        params object[] args)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = (QualityError)result.Error!;
        return problemDetailsFactory.CreateProblemDetails(controller, ToStatusCodeFromQualityError(error), error,
            Localize(messages, result.DomainError.Code, result.Message, args));
    }

    /// <summary>
    ///     Returns the success response, or a 404 response when the query found nothing.
    /// </summary>
    /// <param name="notFoundArgs">The values for the placeholders of the not found message, for example the id.</param>
    public static IActionResult ToActionResultFromQueryResult<T>(
        ControllerBase controller,
        T? entity,
        Error notFoundError,
        QualityError notFoundErrorType,
        IStringLocalizer<QualityMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction,
        params object[] notFoundArgs) where T : class
    {
        if (entity is not null) return successAction(entity);

        return problemDetailsFactory.CreateProblemDetails(controller,
            ToStatusCodeFromQualityError(notFoundErrorType), notFoundErrorType,
            Localize(messages, notFoundError.Code, notFoundError.Message, notFoundArgs));
    }
}
