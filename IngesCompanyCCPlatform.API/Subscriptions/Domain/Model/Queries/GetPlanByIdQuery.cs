using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;

/// <summary>
///     Represents a query to get a plan by its id.
/// </summary>
/// <param name="PlanId">
///     The id of the plan to get.
/// </param>
public record GetPlanByIdQuery(PlanId PlanId)
{
    public GetPlanByIdQuery(int planId) : this(new PlanId(planId))
    {
    }
}
