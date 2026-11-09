using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the material approval repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class MaterialApprovalRepository(AppDbContext context)
    : BaseRepository<MaterialApproval>(context), IMaterialApprovalRepository
{
    // <inheritdoc />
    public async Task<MaterialApproval?> FindByRawMaterialLotIdAsync(RawMaterialLotId rawMaterialLotId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<MaterialApproval>()
            .AsNoTracking()
            .FirstOrDefaultAsync(approval => approval.RawMaterialLotId == rawMaterialLotId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<MaterialApproval?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var approvalId = new MaterialApprovalId(id);
        return await Context.Set<MaterialApproval>()
            .FirstOrDefaultAsync(approval => approval.Id == approvalId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<MaterialApproval?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var approvalId = new MaterialApprovalId(id);
        return await Context.Set<MaterialApproval>()
            .AsNoTracking()
            .FirstOrDefaultAsync(approval => approval.Id == approvalId, cancellationToken);
    }
}
