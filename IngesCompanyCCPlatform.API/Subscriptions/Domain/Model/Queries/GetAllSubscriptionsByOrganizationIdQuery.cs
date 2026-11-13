using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;

/// <summary>
///     Represents a query to get the subscriptions of an organization.
/// </summary>
/// <param name="OrganizationId">
///     The id of the organization.
/// </param>
public record GetAllSubscriptionsByOrganizationIdQuery(OrganizationId OrganizationId)
{
    public GetAllSubscriptionsByOrganizationIdQuery(int organizationId) : this(new OrganizationId(organizationId))
    {
    }
}
