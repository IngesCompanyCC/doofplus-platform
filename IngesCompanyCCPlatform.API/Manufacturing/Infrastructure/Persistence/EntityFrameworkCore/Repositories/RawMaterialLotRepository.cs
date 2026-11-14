using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="RawMaterialLot" />.
/// </summary>
public class RawMaterialLotRepository(AppDbContext context) : BaseRepository<RawMaterialLot>(context), IRawMaterialLotRepository
{
    /// <inheritdoc />
    public override async Task<RawMaterialLot?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var lotId = new RawMaterialLotId(id);
        return await Context.Set<RawMaterialLot>().FirstOrDefaultAsync(lot => lot.Id == lotId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<RawMaterialLot?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var lotId = new RawMaterialLotId(id);
        return await Context.Set<RawMaterialLot>().AsNoTracking().FirstOrDefaultAsync(lot => lot.Id == lotId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<RawMaterialLot>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<RawMaterialLot>().AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(RawMaterialLotCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<RawMaterialLot>().AnyAsync(lot => lot.Code == code, cancellationToken);
    }
}
