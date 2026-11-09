using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Filters;

/// <summary>
///     Answers with a 400 problem details when the data of a request is rejected while building its command.
/// </summary>
/// <remarks>
///     The value objects of Quality &amp; Compliance (ids, specification range, root cause, period) throw
///     <see cref="ArgumentException" /> for invalid data. Without this filter the global exception handler
///     would answer with a 500.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public sealed class QualityInvalidDataFilterAttribute : ActionFilterAttribute
{
    /// <inheritdoc />
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not ArgumentException exception || context.Controller is not ControllerBase controller)
            return;

        var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
        context.Result = QualityActionResultAssembler.ToBadRequest(controller, problemDetailsFactory, exception);
        context.ExceptionHandled = true;
    }
}
