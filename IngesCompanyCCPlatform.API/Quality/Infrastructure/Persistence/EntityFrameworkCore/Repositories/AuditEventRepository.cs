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
    public async Task<IEnumerable<AuditEvent>> FindAllByAggregateTypeAggregateIdAsNoTrackingAsync(string? aggregateType, int? aggregateId, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<AuditEvent>().AsNoTracking();
        if (aggregateType != null) query = query.Where(auditEvent => auditEvent.AggregateType == aggregateType);
        if (aggregateId is not null) query = query.Where(auditEvent => auditEvent.AggregateId == aggregateId);
        return await query.OrderByDescending(auditEvent => auditEvent.OccurredAt).ToListAsync(cancellationToken);
    }
}
