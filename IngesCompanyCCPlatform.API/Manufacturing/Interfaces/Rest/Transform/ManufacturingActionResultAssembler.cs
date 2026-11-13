using Microsoft.Extensions.Localization;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Manufacturing.Resources;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;

public static class ManufacturingActionResultAssembler
{
    private static int ToStatusCodeFromManufacturingError(ManufacturingError error)
    {
        return error switch
        {
            ManufacturingError.BatchNotFound => StatusCodes.Status404NotFound,
            ManufacturingError.DuplicateBatchNumber => StatusCodes.Status409Conflict,
            ManufacturingError.BatchAlreadyClosed => StatusCodes.Status409Conflict,
            ManufacturingError.BatchNotPlanned => StatusCodes.Status400BadRequest,
            ManufacturingError.InvalidOperation => StatusCodes.Status400BadRequest,
            ManufacturingError.DatabaseError => StatusCodes.Status500InternalServerError,
            ManufacturingError.InternalServerError => StatusCodes.Status500InternalServerError,
            ManufacturingError.ProductNotFound => StatusCodes.Status404NotFound,
            ManufacturingError.MasterFormulaNotFound => StatusCodes.Status404NotFound,
            ManufacturingError.ProductionOrderNotFound => StatusCodes.Status404NotFound,
            ManufacturingError.OperationNotFound => StatusCodes.Status404NotFound,
            ManufacturingError.RawMaterialLotNotFound => StatusCodes.Status404NotFound,
            ManufacturingError.DuplicateProductCode => StatusCodes.Status409Conflict,
            ManufacturingError.DuplicateFormulaVersion => StatusCodes.Status409Conflict,
            ManufacturingError.InvalidFormulaState => StatusCodes.Status409Conflict,
            ManufacturingError.FormulaNotApproved => StatusCodes.Status409Conflict,
            ManufacturingError.DuplicateProductionOrderCode => StatusCodes.Status409Conflict,
            ManufacturingError.InvalidProductionOrderState => StatusCodes.Status409Conflict,
            ManufacturingError.DuplicateRawMaterialLotCode => StatusCodes.Status409Conflict,
            ManufacturingError.InvalidRawMaterialLotState => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResult<T>(
        ControllerBase controller,
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = result.Error is ManufacturingError manufacturingError
            ? manufacturingError
            : ManufacturingError.None;

        var statusCode = ToStatusCodeFromManufacturingError(error);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    /// <summary>
    ///     Gets the localized text of a message key, or the fallback when the key has no translation.
    /// </summary>
    private static string Localize(IStringLocalizer<ManufacturingMessages> messages, string key, string fallback,
        params object[] args)
    {
        var message = messages[key, args];
        return message.ResourceNotFound ? fallback : message.Value;
    }

    /// <summary>
    ///     Returns the success response, or the problem details of the failed command with its detail localized
    ///     from <see cref="ManufacturingMessages" />.
    /// </summary>
    public static IActionResult ToActionResultFromCommandResult<T>(
        ControllerBase controller,
        Result<T> result,
        IStringLocalizer<ManufacturingMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = result.Error is ManufacturingError manufacturingError ? manufacturingError : ManufacturingError.None;
        return problemDetailsFactory.CreateProblemDetails(controller, ToStatusCodeFromManufacturingError(error),
            result.Error, Localize(messages, result.DomainError.Code, result.Message));
    }

    /// <summary>
    ///     Returns the success response, or a 404 response when the query found nothing.
    /// </summary>
    public static IActionResult ToActionResultFromQueryResult<T>(
        ControllerBase controller,
        T? entity,
        Error notFoundError,
        ManufacturingError notFoundErrorType,
        IStringLocalizer<ManufacturingMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction,
        params object[] notFoundArgs) where T : class
    {
        if (entity is not null) return successAction(entity);

        return problemDetailsFactory.CreateProblemDetails(controller,
            ToStatusCodeFromManufacturingError(notFoundErrorType), notFoundErrorType,
            Localize(messages, notFoundError.Code, notFoundError.Message, notFoundArgs));
    }
}
