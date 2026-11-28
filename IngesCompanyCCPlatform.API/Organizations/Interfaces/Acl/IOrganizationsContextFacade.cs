namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Acl;

/// <summary>
///     Anti-corruption layer of Organizations: what other bounded contexts can ask about an organization
///     without using its domain model.
/// </summary>
public interface IOrganizationsContextFacade
{
    /// <summary>
    ///     Tells whether an organization with the given id is registered.
    /// </summary>
    Task<bool> ExistsOrganizationById(int organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the legal name of an organization.
    /// </summary>
    Task<string> FetchLegalNameById(int organizationId, CancellationToken cancellationToken = default);
}