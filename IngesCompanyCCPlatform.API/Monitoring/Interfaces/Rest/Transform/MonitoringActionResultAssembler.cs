using IngesCompanyCCPlatform.API.Monitoring.Domain.Model;
using IngesCompanyCCPlatform.API.Monitoring.Resources;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;

/// <summary>
///     Converts the results of the IoT Monitoring services into HTTP responses.
/// </summary>
/// <remarks>
///     The title of each problem details response comes from <c>ErrorMessages</c>
///     (<c>MonitoringError.&lt;Case&gt;</c>) and the detail from <see cref="MonitoringMessages" />, whose keys are the
///     codes of the domain errors. Invalid data (the keys thrown by the value objects) is answered with a 400 by the
///     global exception handler of Shared.
/// </remarks>
public static class MonitoringActionResultAssembler
{
    private static int ToStatusCodeFromMonitoringError(MonitoringError error)
    {
        return error switch
        {
            MonitoringError.EquipmentNotFound => StatusCodes.Status404NotFound,
            MonitoringError.SensorNotFound => StatusCodes.Status404NotFound,
            MonitoringError.ReadingNotFound => StatusCodes.Status404NotFound,
            MonitoringError.AlertNotFound => StatusCodes.Status404NotFound,
            MonitoringError.DuplicateEquipmentCode => StatusCodes.Status409Conflict,
            MonitoringError.EquipmentInUse => StatusCodes.Status409Conflict,
            MonitoringError.DuplicateSensorCode => StatusCodes.Status409Conflict,
            MonitoringError.DuplicateDeviceId => StatusCodes.Status409Conflict,
            MonitoringError.DuplicateAlertCode => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }

    /// <summary>
    ///     Gets the localized text of a message key, or the fallback when the key has no translation.
    /// </summary>
    public static string Localize(IStringLocalizer<MonitoringMessages> messages, string key, string fallback,
        params object[] args)
    {
        var message = messages[key, args];
        return message.ResourceNotFound ? fallback : message.Value;
    }

    /// <summary>
    ///     Returns the success response, or the problem details of the failed command.
    /// </summary>
    public static IActionResult ToActionResultFromCommandResult<T>(
        ControllerBase controller,
        Result<T> result,
        IStringLocalizer<MonitoringMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = (MonitoringError)result.Error!;
        return problemDetailsFactory.CreateProblemDetails(controller, ToStatusCodeFromMonitoringError(error), error,
            Localize(messages, result.DomainError.Code, result.Message));
    }

    /// <summary>
    ///     Returns the success response, or a 404 response when the query found nothing.
    /// </summary>
    /// <param name="notFoundArgs">The values for the placeholders of the not found message, for example the id.</param>
    public static IActionResult ToActionResultFromQueryResult<T>(
        ControllerBase controller,
        T? entity,
        Error notFoundError,
        MonitoringError notFoundErrorType,
        IStringLocalizer<MonitoringMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction,
        params object[] notFoundArgs) where T : class
    {
        if (entity is not null) return successAction(entity);

        return problemDetailsFactory.CreateProblemDetails(controller,
            ToStatusCodeFromMonitoringError(notFoundErrorType), notFoundErrorType,
            Localize(messages, notFoundError.Code, notFoundError.Message, notFoundArgs));
    }
}
