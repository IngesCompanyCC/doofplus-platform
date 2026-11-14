using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

/// <summary>
///     Repository of the equipment of the plant.
/// </summary>
public interface IEquipmentRepository : IBaseRepository<Equipment>
{
    /// <summary>
    ///     Checks whether a piece of equipment already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(EquipmentCode code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a piece of equipment by its code, without tracking it.
    /// </summary>
    Task<Equipment?> FindByCodeAsNoTrackingAsync(EquipmentCode code, CancellationToken cancellationToken = default);
}
