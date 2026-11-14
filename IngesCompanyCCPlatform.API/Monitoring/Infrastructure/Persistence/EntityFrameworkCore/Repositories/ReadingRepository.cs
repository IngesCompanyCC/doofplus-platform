using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Reading" />.
/// </summary>
public class ReadingRepository(AppDbContext context) : BaseRepository<Reading>(context), IReadingRepository
{
    /// <inheritdoc />
    public new async Task<Reading?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var readingId = new ReadingId(id);
        return await Context.Set<Reading>().FirstOrDefaultAsync(reading => reading.Id == readingId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<Reading?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var readingId = new ReadingId(id);
        return await Context.Set<Reading>()
            .AsNoTracking()
            .FirstOrDefaultAsync(reading => reading.Id == readingId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Reading>> FindAllBySensorCodeAsNoTrackingAsync(SensorCode sensorCode,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Reading>()
            .AsNoTracking()
            .Where(reading => reading.SensorCode == sensorCode)
            .OrderByDescending(reading => reading.RecordedAt)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Reading>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Reading>()
            .AsNoTracking()
            .Where(reading => reading.BatchCode == batchCode)
            .OrderByDescending(reading => reading.RecordedAt)
            .ToListAsync(cancellationToken);
    }
}
