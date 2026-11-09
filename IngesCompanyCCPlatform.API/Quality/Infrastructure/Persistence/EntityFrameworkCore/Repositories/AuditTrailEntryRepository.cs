using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the audit trail repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class AuditTrailEntryRepository(AppDbContext context)
    : BaseRepository<AuditTrailEntry>(context), IAuditTrailEntryRepository
{
    // <inheritdoc />
    public async Task<IReadOnlyCollection<AuditTrailEntry>> FindByEntityAsync(string entityType, string entityId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<AuditTrailEntry>()
            .AsNoTracking()
            .Where(entry => entry.EntityType == entityType && entry.EntityId == entityId)
            .OrderByDescending(entry => entry.ChangedAt)
            .ToListAsync(cancellationToken);
    }

    // <inheritdoc />
    public new async Task<AuditTrailEntry?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var entryId = new AuditTrailEntryId(id);
        return await Context.Set<AuditTrailEntry>()
            .AsNoTracking()
            .FirstOrDefaultAsync(entry => entry.Id == entryId, cancellationToken);
    }
}
