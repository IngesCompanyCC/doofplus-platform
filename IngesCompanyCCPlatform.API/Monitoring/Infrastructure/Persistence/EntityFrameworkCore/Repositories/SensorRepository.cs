using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Sensor" />.
/// </summary>
public class SensorRepository(AppDbContext context) : BaseRepository<Sensor>(context), ISensorRepository
{
    /// <inheritdoc />
    public new async Task<Sensor?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var sensorId = new SensorId(id);
        return await Context.Set<Sensor>().FirstOrDefaultAsync(sensor => sensor.Id == sensorId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<Sensor?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var sensorId = new SensorId(id);
        return await Context.Set<Sensor>()
            .AsNoTracking()
            .FirstOrDefaultAsync(sensor => sensor.Id == sensorId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(SensorCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sensor>().AnyAsync(sensor => sensor.Code == code, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByDeviceIdAsync(DeviceId deviceId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sensor>().AnyAsync(sensor => sensor.DeviceId == deviceId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Sensor?> FindByCodeAsNoTrackingAsync(SensorCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sensor>()
            .AsNoTracking()
            .FirstOrDefaultAsync(sensor => sensor.Code == code, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Sensor?> FindByDeviceIdAsNoTrackingAsync(DeviceId deviceId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sensor>()
            .AsNoTracking()
            .FirstOrDefaultAsync(sensor => sensor.DeviceId == deviceId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sensor>> FindAllByEquipmentCodeAsNoTrackingAsync(EquipmentCode equipmentCode,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sensor>()
            .AsNoTracking()
            .Where(sensor => sensor.EquipmentCode == equipmentCode)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAssignedToBatchByEquipmentCodeAsync(EquipmentCode equipmentCode,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Sensor>()
            .AnyAsync(sensor => sensor.EquipmentCode == equipmentCode && sensor.AssignedBatch != "", cancellationToken);
    }
}
