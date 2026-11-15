using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="QualityDocument" />.
/// </summary>
public class QualityDocumentRepository(AppDbContext context) : BaseRepository<QualityDocument>(context), IQualityDocumentRepository
{
    /// <inheritdoc />
    public new async Task<QualityDocument?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var qualityDocumentId = new QualityDocumentId(id);
        return await Context.Set<QualityDocument>().FirstOrDefaultAsync(qualityDocument => qualityDocument.Id == qualityDocumentId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<QualityDocument?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var qualityDocumentId = new QualityDocumentId(id);
        return await Context.Set<QualityDocument>()
            .AsNoTracking()
            .FirstOrDefaultAsync(qualityDocument => qualityDocument.Id == qualityDocumentId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QualityDocument>> FindAllByCodeAsNoTrackingAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<QualityDocument>()
            .AsNoTracking()
            .Where(qualityDocument => qualityDocument.Code == code)
            .ToListAsync(cancellationToken);
    }
}
