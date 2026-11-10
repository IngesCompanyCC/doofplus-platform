using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;

/// <summary>
///     Converts the results of the Subscriptions services into HTTP responses.
/// </summary>
public static class SubscriptionsActionResultAssembler
{
    /// <summary>
    ///     Maps an error of the Subscriptions bounded context to its HTTP status code.
    /// </summary>
    private static int ToStatusCodeFromSubscriptionsError(SubscriptionsError error)
    {
        return error switch
        {
            SubscriptionsError.PlanNotFound => StatusCodes.Status404NotFound,
            SubscriptionsError.SubscriptionNotFound => StatusCodes.Status404NotFound,
            SubscriptionsError.DuplicatePlanKey => StatusCodes.Status409Conflict,
            SubscriptionsError.AlreadySubscribed => StatusCodes.Status409Conflict,
            SubscriptionsError.InvalidSubscriptionState => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
    }

    /// <summary>
    ///     Returns the success response, or the problem details of the failed command.
    /// </summary>
    public static IActionResult ToActionResultFromCommandResult<T>(
        ControllerBase controller,
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = (SubscriptionsError)result.Error!;
        return problemDetailsFactory.CreateProblemDetails(controller, ToStatusCodeFromSubscriptionsError(error),
            error, result.Message);
    }

    /// <summary>
    ///     Returns the success response, or a 404 response when the query found nothing.
    /// </summary>
    public static IActionResult ToActionResultFromQueryResult<T>(
        ControllerBase controller,
        T? entity,
        Error notFoundError,
        SubscriptionsError notFoundErrorType,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction) where T : class
    {
        if (entity is not null) return successAction(entity);

        return problemDetailsFactory.CreateProblemDetails(controller,
            ToStatusCodeFromSubscriptionsError(notFoundErrorType), notFoundErrorType, notFoundError.Message);
    }

    /// <summary>
    ///     Returns a 400 response for a request whose data breaks a rule of a value object or an aggregate.
    /// </summary>
    /// <param name="exception">
    ///     The exception thrown by the domain; its message is the key of the error.
    /// </param>
    public static IActionResult ToActionResultFromInvalidArgument(
        ControllerBase controller,
        ArgumentException exception,
        ProblemDetailsFactory problemDetailsFactory)
    {
        return problemDetailsFactory.CreateProblemDetails(controller, StatusCodes.Status400BadRequest, (Enum?)null,
            exception.Message);
    }
}
