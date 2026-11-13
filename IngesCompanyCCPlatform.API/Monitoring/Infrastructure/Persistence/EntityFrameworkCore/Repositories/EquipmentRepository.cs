using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Equipment" />.
/// </summary>
public class EquipmentRepository(AppDbContext context) : BaseRepository<Equipment>(context), IEquipmentRepository
{
    /// <inheritdoc />
    public new async Task<Equipment?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var equipmentId = new EquipmentId(id);
        return await Context.Set<Equipment>().FirstOrDefaultAsync(equipment => equipment.Id == equipmentId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<Equipment?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var equipmentId = new EquipmentId(id);
        return await Context.Set<Equipment>()
            .AsNoTracking()
            .FirstOrDefaultAsync(equipment => equipment.Id == equipmentId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(EquipmentCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Equipment>().AnyAsync(equipment => equipment.Code == code, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Equipment?> FindByCodeAsNoTrackingAsync(EquipmentCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Equipment>()
            .AsNoTracking()
            .FirstOrDefaultAsync(equipment => equipment.Code == code, cancellationToken);
    }
}
