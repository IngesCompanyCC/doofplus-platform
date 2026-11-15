using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="AuditEvent" />.
/// </summary>
public class AuditEventRepository(AppDbContext context) : BaseRepository<AuditEvent>(context), IAuditEventRepository
{
    /// <inheritdoc />
    public new async Task<AuditEvent?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var auditEventId = new AuditEventId(id);
        return await Context.Set<AuditEvent>().FirstOrDefaultAsync(auditEvent => auditEvent.Id == auditEventId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<AuditEvent?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var auditEventId = new AuditEventId(id);
        return await Context.Set<AuditEvent>()
            .AsNoTracking()
            .FirstOrDefaultAsync(auditEvent => auditEvent.Id == auditEventId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditEvent>> FindAllByRecordAsNoTrackingAsync(string record, CancellationToken cancellationToken = default)
    {
        return await Context.Set<AuditEvent>()
            .AsNoTracking()
            .Where(auditEvent => auditEvent.Record == record)
            .ToListAsync(cancellationToken);
    }
}
