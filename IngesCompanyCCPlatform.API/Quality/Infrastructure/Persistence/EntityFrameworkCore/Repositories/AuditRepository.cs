using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Audit" />.
/// </summary>
public class AuditRepository(AppDbContext context) : BaseRepository<Audit>(context), IAuditRepository
{
    /// <inheritdoc />
    public new async Task<Audit?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var auditId = new AuditId(id);
        return await Context.Set<Audit>().FirstOrDefaultAsync(audit => audit.Id == auditId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<Audit?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var auditId = new AuditId(id);
        return await Context.Set<Audit>()
            .AsNoTracking()
            .FirstOrDefaultAsync(audit => audit.Id == auditId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Audit>().AnyAsync(audit => audit.Code == code, cancellationToken);
    }
}
