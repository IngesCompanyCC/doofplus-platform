using IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Equipment" />.
/// </summary>
public class EquipmentQueryService(IEquipmentRepository equipmentRepository) : IEquipmentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Equipment>> Handle(GetAllEquipmentQuery query, CancellationToken cancellationToken)
    {
        return await equipmentRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Equipment?> Handle(GetEquipmentByIdQuery query, CancellationToken cancellationToken)
    {
        return await equipmentRepository.FindByIdAsNoTrackingAsync(query.EquipmentId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Equipment?> Handle(GetEquipmentByCodeQuery query, CancellationToken cancellationToken)
    {
        return await equipmentRepository.FindByCodeAsNoTrackingAsync(query.EquipmentCode, cancellationToken);
    }
}
