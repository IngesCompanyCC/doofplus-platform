using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;

/// <summary>
///     Converts the results of the Subscriptions services into HTTP responses.
/// </summary>
/// <remarks>
///     The title of each problem details response comes from <c>ErrorMessages</c>
///     (<c>SubscriptionsError.&lt;Case&gt;</c>) and the detail from <see cref="SubscriptionsMessages" />,
///     whose keys are the codes of the domain errors.
/// </remarks>
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
    ///     Gets the localized text of a message key, or the fallback when the key has no translation.
    /// </summary>
    private static string Localize(IStringLocalizer<SubscriptionsMessages> messages, string key, string fallback,
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
        IStringLocalizer<SubscriptionsMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = (SubscriptionsError)result.Error!;
        return problemDetailsFactory.CreateProblemDetails(controller, ToStatusCodeFromSubscriptionsError(error),
            error, Localize(messages, result.DomainError.Code, result.Message));
    }

    /// <summary>
    ///     Returns the success response, or a 404 response when the query found nothing.
    /// </summary>
    /// <param name="notFoundArgs">
    ///     The values for the placeholders of the not found message, for example the id.
    /// </param>
    public static IActionResult ToActionResultFromQueryResult<T>(
        ControllerBase controller,
        T? entity,
        Error notFoundError,
        SubscriptionsError notFoundErrorType,
        IStringLocalizer<SubscriptionsMessages> messages,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction,
        params object[] notFoundArgs) where T : class
    {
        if (entity is not null) return successAction(entity);

        return problemDetailsFactory.CreateProblemDetails(controller,
            ToStatusCodeFromSubscriptionsError(notFoundErrorType), notFoundErrorType,
            Localize(messages, notFoundError.Code, notFoundError.Message, notFoundArgs));
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
        IStringLocalizer<SubscriptionsMessages> messages,
        ProblemDetailsFactory problemDetailsFactory)
    {
        return problemDetailsFactory.CreateProblemDetails(controller, StatusCodes.Status400BadRequest, (Enum?)null,
            Localize(messages, exception.Message, exception.Message));
    }
}
