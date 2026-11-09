using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.QueryServices;

/// <summary>
///     Represents the plan query service of the Subscriptions bounded context.
/// </summary>
public interface IPlanQueryService
{
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken);

    Task<Plan?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken);
}
