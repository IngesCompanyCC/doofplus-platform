using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.QueryServices;

/// <summary>
///     Handles organization read use cases.
/// </summary>
public class OrganizationQueryService(
    IOrganizationRepository organizationRepository) : IOrganizationQueryService
{
    public async Task<Organization?> Handle(
        GetOrganizationByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await organizationRepository.FindByIdAsync(query.OrganizationId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Organization>> Handle(
        GetAllOrganizationsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await organizationRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Organization?> Handle(
        GetOrganizationByRucQuery query,
        CancellationToken cancellationToken = default)
    {
        return await organizationRepository.FindByRucAsync(query.Ruc.Trim(), cancellationToken);
    }
}