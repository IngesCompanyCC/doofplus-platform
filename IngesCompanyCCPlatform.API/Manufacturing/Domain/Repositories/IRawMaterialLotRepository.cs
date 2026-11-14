using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

/// <summary>
///     Repository of the lots of raw material.
/// </summary>
public interface IRawMaterialLotRepository : IBaseRepository<RawMaterialLot>
{
    /// <summary>
    ///     Checks whether a lot already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(RawMaterialLotCode code, CancellationToken cancellationToken = default);
}
