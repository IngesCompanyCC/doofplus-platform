using IngesCompanyCCPlatform.API.Iam.Domain.Model;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Turns the results of the IAM services into responses. The detail of a 401 is its reason:
///     invalid-credentials, two-factor-required or invalid-two-factor-code.
/// </summary>
public static class IamActionResultAssembler
{
    private static int ToStatusCodeFromIamError(IamError error) => error switch
    {
        IamError.InvalidCredentials => StatusCodes.Status401Unauthorized,
        IamError.TwoFactorRequired => StatusCodes.Status401Unauthorized,
        IamError.InvalidTwoFactorCode => StatusCodes.Status401Unauthorized,
        IamError.EmailAlreadyTaken => StatusCodes.Status409Conflict,
        IamError.UserNotFound => StatusCodes.Status404NotFound,
        IamError.InvitationNotFound => StatusCodes.Status404NotFound,
        IamError.InvitationRequired => StatusCodes.Status403Forbidden,
        IamError.InvalidUserData => StatusCodes.Status400BadRequest,
        IamError.DatabaseError => StatusCodes.Status500InternalServerError,
        IamError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResult<T>(
        ControllerBase controller,
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var error = result.Error is IamError iamError ? iamError : IamError.None;
        return problemDetailsFactory.CreateProblemDetails(controller, ToStatusCodeFromIamError(error), result.Error,
            result.Message);
    }

    public static IActionResult UserNotFound(ControllerBase controller, ProblemDetailsFactory problemDetailsFactory,
        int userId) =>
        problemDetailsFactory.CreateProblemDetails(controller, StatusCodes.Status404NotFound, IamError.UserNotFound,
            $"User not found: {userId}");
}
