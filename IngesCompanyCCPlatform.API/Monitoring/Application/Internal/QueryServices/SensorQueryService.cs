using IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Sensor" />.
/// </summary>
public class SensorQueryService(ISensorRepository sensorRepository) : ISensorQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Sensor>> Handle(GetAllSensorsQuery query, CancellationToken cancellationToken)
    {
        return await sensorRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sensor>> Handle(GetAllSensorsByEquipmentCodeQuery query, CancellationToken cancellationToken)
    {
        return await sensorRepository.FindAllByEquipmentCodeAsNoTrackingAsync(query.EquipmentCode, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Sensor?> Handle(GetSensorByIdQuery query, CancellationToken cancellationToken)
    {
        return await sensorRepository.FindByIdAsNoTrackingAsync(query.SensorId, cancellationToken);
    }
}
