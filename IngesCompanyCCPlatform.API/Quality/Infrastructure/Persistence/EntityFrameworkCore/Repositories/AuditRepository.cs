using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the audit repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Every read loads the findings of the audit.
/// </remarks>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class AuditRepository(AppDbContext context) : BaseRepository<Audit>(context), IAuditRepository
{
    private IQueryable<Audit> Audits => Context.Set<Audit>().Include(audit => audit.Findings);

    // <inheritdoc />
    public new async Task<Audit?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var auditId = new AuditId(id);
        return await Audits.FirstOrDefaultAsync(audit => audit.Id == auditId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Audit?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var auditId = new AuditId(id);
        return await Audits.AsNoTracking().FirstOrDefaultAsync(audit => audit.Id == auditId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<IEnumerable<Audit>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Audits.AsNoTracking().ToListAsync(cancellationToken);
    }
}
