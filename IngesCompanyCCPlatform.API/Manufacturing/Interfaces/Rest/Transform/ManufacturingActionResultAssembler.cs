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
}
