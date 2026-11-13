using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Errors;

/// <summary>
///     Domain errors of the plan aggregate. Each code is the key of its localized message.
/// </summary>
public static class PlanErrors
{
    public static readonly Error DuplicatePlanKey =
        Error.Conflict("plan.error.duplicate", "A plan with the same planKey already exists.");

    public static Error NotFoundById(int id) =>
        Error.NotFound("plan.error.notFoundById", $"Plan not found for id {id}.");
}
