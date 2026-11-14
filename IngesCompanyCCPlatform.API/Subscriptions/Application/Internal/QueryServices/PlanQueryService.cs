using IngesCompanyCCPlatform.API.Subscriptions.Application.QueryServices;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.QueryServices;

/// <summary>
///     Implements the plan query service of the Subscriptions bounded context.
/// </summary>
public class PlanQueryService(IPlanRepository planRepository) : IPlanQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken)
    {
        return await planRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Plan?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await planRepository.FindByIdAsNoTrackingAsync(query.PlanId, cancellationToken);
    }
}
