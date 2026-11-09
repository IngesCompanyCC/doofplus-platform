using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Turns the results of the Quality &amp; Compliance services into HTTP responses.
/// </summary>
/// <remarks>
///     A failure becomes a localized problem details response; its title is the text of the
///     <see cref="QualityError" /> case in <c>ErrorMessages</c> and its detail is the message of the domain error.
/// </remarks>
public static class QualityActionResultAssembler
{
    /// <summary>
    ///     Maps an error of the bounded context to its HTTP status code.
    /// </summary>
    /// <param name="error">The error of the bounded context.</param>
    /// <returns>The HTTP status code.</returns>
    public static int ToStatusCodeFromQualityError(QualityError error)
    {
        return error switch
        {
            QualityError.InvalidData => StatusCodes.Status400BadRequest,
            QualityError.QualityDocumentNotFound or QualityError.MaterialApprovalNotFound
                or QualityError.AnalyticalResultNotFound or QualityError.BatchReviewNotFound
                or QualityError.DeviationNotFound or QualityError.AuditNotFound
                or QualityError.RegulatoryReportNotFound => StatusCodes.Status404NotFound,
            QualityError.InvalidState or QualityError.DuplicateQualityDocumentCode
                or QualityError.MaterialApprovalAlreadyRequested or QualityError.BatchReviewAlreadyStarted
                or QualityError.DuplicateDeviationCode => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }

    /// <summary>
    ///     Returns the success response, or the problem details of the failure.
    /// </summary>
    public static IActionResult ToActionResultFromResult<T>(
        ControllerBase controller,
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = result.Error as QualityError? ?? QualityError.None;
        return problemDetailsFactory.CreateProblemDetails(controller, ToStatusCodeFromQualityError(error),
            result.Error, result.Message);
    }

    /// <summary>
    ///     Returns the success response, or a not found problem details when the aggregate does not exist.
    /// </summary>
    public static IActionResult ToActionResultFromQuery<T>(
        ControllerBase controller,
        T? entity,
        ProblemDetailsFactory problemDetailsFactory,
        QualityError notFoundError,
        string notFoundMessage,
        Func<T, IActionResult> successAction) where T : class
    {
        if (entity is not null) return successAction(entity);

        return problemDetailsFactory.CreateProblemDetails(controller, StatusCodes.Status404NotFound, notFoundError,
            notFoundMessage);
    }

    /// <summary>
    ///     Returns a bad request problem details for data rejected while building a command.
    /// </summary>
    public static IActionResult ToBadRequest(ControllerBase controller, ProblemDetailsFactory problemDetailsFactory,
        ArgumentException exception)
    {
        return problemDetailsFactory.CreateProblemDetails(controller, StatusCodes.Status400BadRequest,
            QualityError.InvalidData, exception.Message);
    }
}
