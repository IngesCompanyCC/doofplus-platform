using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;


namespace IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;

/// <summary>
///     Application service for Organization read use cases.
/// </summary>
public interface IOrganizationQueryService
{
    Task<Organization?> Handle(
        GetOrganizationByIdQuery query,
        CancellationToken cancellationToken = default);
}