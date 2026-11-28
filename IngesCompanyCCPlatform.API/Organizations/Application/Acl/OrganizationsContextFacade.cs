using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Acl;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Acl;

/// <summary>
///     Answers the questions of other bounded contexts about organizations through the query service.
/// </summary>
public class OrganizationsContextFacade(IOrganizationQueryService organizationQueryService)
    : IOrganizationsContextFacade
{
    /// <inheritdoc />
    public async Task<bool> ExistsOrganizationById(int organizationId, CancellationToken cancellationToken = default)
    {
        if (organizationId <= 0) return false;

        var organization = await organizationQueryService.Handle(
            new GetOrganizationByIdQuery(organizationId), cancellationToken);

        return organization is not null;
    }

    /// <inheritdoc />
    public async Task<string> FetchLegalNameById(int organizationId, CancellationToken cancellationToken = default)
    {
        if (organizationId <= 0) return string.Empty;

        var organization = await organizationQueryService.Handle(
            new GetOrganizationByIdQuery(organizationId), cancellationToken);

        return organization?.LegalName ?? string.Empty;
    }
}