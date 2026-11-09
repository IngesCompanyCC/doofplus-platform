using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the quality document repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Every read loads the versions of the document.
/// </remarks>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class QualityDocumentRepository(AppDbContext context)
    : BaseRepository<QualityDocument>(context), IQualityDocumentRepository
{
    private IQueryable<QualityDocument> Documents => Context.Set<QualityDocument>().Include(d => d.Versions);

    // <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<QualityDocument>().AnyAsync(document => document.Code == code, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<QualityDocument?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var documentId = new QualityDocumentId(id);
        return await Documents.FirstOrDefaultAsync(document => document.Id == documentId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<QualityDocument?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var documentId = new QualityDocumentId(id);
        return await Documents.AsNoTracking()
            .FirstOrDefaultAsync(document => document.Id == documentId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<IEnumerable<QualityDocument>> ListAsNoTrackingAsync(
        CancellationToken cancellationToken = default)
    {
        return await Documents.AsNoTracking().ToListAsync(cancellationToken);
    }
}
