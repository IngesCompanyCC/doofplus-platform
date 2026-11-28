namespace IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.Acl;

/// <summary>
///     What Subscriptions needs to know about organizations, which belong to the Organizations bounded context.
/// </summary>
public interface IExternalOrganizationService
{
    /// <summary>
    ///     Tells whether the organization that wants to subscribe is registered.
    /// </summary>
    Task<bool> ExistsOrganization(int organizationId, CancellationToken cancellationToken = default);
}