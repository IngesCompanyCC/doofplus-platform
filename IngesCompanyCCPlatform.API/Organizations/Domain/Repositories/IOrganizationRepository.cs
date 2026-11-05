using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

/// <summary>
///     Repository contract for the Organization aggregate.
/// </summary>
public interface IOrganizationRepository : IBaseRepository<Organization>
{
    Task<Organization?> FindByRucAsync(string ruc, CancellationToken cancellationToken = default);

    Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken = default);
}