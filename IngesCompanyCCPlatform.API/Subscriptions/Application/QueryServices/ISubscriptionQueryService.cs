using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.QueryServices;

/// <summary>
///     Represents the subscription query service of the Subscriptions bounded context.
/// </summary>
public interface ISubscriptionQueryService
{
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByOrganizationIdQuery query,
        CancellationToken cancellationToken);
}
