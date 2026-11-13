using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class OrganizationsActionResultAssembler
{
    private static int ToStatusCodeFromOrganizationsError(OrganizationsError error)
    {
        return error switch
        {
            OrganizationsError.OrganizationNotFound => StatusCodes.Status404NotFound,
            OrganizationsError.UserProfileNotFound => StatusCodes.Status404NotFound,
            OrganizationsError.FacilityNotFound => StatusCodes.Status404NotFound,
            OrganizationsError.ContactInquiryNotFound => StatusCodes.Status404NotFound,
            OrganizationsError.DuplicateRuc => StatusCodes.Status409Conflict,
            OrganizationsError.DuplicateUserEmail => StatusCodes.Status409Conflict,
            OrganizationsError.OrganizationAlreadyActive => StatusCodes.Status409Conflict,
            OrganizationsError.OrganizationSuspended => StatusCodes.Status409Conflict,
            OrganizationsError.InvalidContactInquiryData => StatusCodes.Status400BadRequest,
            OrganizationsError.InvalidOperation => StatusCodes.Status400BadRequest,
            OrganizationsError.DatabaseError => StatusCodes.Status500InternalServerError,
            OrganizationsError.InternalServerError => StatusCodes.Status500InternalServerError,
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

        var error = result.Error is OrganizationsError organizationsError
            ? organizationsError
            : OrganizationsError.None;

        var statusCode = ToStatusCodeFromOrganizationsError(error);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
