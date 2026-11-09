using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the batch review repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Every read loads the evidences and the certificate of the review.
/// </remarks>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class BatchReviewRepository(AppDbContext context)
    : BaseRepository<BatchReview>(context), IBatchReviewRepository
{
    private IQueryable<BatchReview> Reviews => Context.Set<BatchReview>()
        .Include(review => review.Evidences)
        .Include(review => review.Certificate);

    // <inheritdoc />
    public async Task<BatchReview?> FindByProductionBatchIdAsync(ProductionBatchId productionBatchId,
        CancellationToken cancellationToken = default)
    {
        return await Reviews.AsNoTracking()
            .FirstOrDefaultAsync(review => review.ProductionBatchId == productionBatchId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<BatchReview?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var reviewId = new BatchReviewId(id);
        return await Reviews.FirstOrDefaultAsync(review => review.Id == reviewId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<BatchReview?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var reviewId = new BatchReviewId(id);
        return await Reviews.AsNoTracking().FirstOrDefaultAsync(review => review.Id == reviewId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<IEnumerable<BatchReview>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Reviews.AsNoTracking().ToListAsync(cancellationToken);
    }
}
