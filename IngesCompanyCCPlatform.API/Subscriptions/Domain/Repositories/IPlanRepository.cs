using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;

/// <summary>
///     Represents the plan repository of the Subscriptions bounded context.
/// </summary>
public interface IPlanRepository : IBaseRepository<Plan>
{
    /// <summary>
    ///     Checks whether a plan with the given business key exists.
    /// </summary>
    /// <param name="planKey">
    ///     The business key of the plan.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     <c>true</c> when a plan already uses the key.
    /// </returns>
    Task<bool> ExistsByPlanKeyAsync(PlanKey planKey, CancellationToken cancellationToken = default);
}
