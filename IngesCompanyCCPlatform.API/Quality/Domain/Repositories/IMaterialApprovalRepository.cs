using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the material approval repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IMaterialApprovalRepository : IBaseRepository<MaterialApproval>
{
    /// <summary>
    ///     Finds the material approval of a raw material lot.
    /// </summary>
    /// <param name="rawMaterialLotId">The raw material lot.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The material approval, or <c>null</c> when the lot has none.</returns>
    Task<MaterialApproval?> FindByRawMaterialLotIdAsync(RawMaterialLotId rawMaterialLotId,
        CancellationToken cancellationToken = default);
}
