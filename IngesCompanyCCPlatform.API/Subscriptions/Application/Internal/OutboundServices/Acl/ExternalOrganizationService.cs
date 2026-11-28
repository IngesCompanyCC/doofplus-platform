using IngesCompanyCCPlatform.API.Organizations.Interfaces.Acl;
using IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.Acl;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.OutboundServices.Acl;

/// <summary>
///     Asks the Organizations bounded context through its facade.
/// </summary>
public class ExternalOrganizationService(IOrganizationsContextFacade organizationsContextFacade)
    : IExternalOrganizationService
{
    /// <inheritdoc />
    public Task<bool> ExistsOrganization(int organizationId, CancellationToken cancellationToken = default)
    {
        return organizationsContextFacade.ExistsOrganizationById(organizationId, cancellationToken);
    }
}