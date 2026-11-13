using IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Reading" />.
/// </summary>
public class ReadingQueryService(IReadingRepository readingRepository) : IReadingQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Reading>> Handle(GetAllReadingsQuery query, CancellationToken cancellationToken)
    {
        return await readingRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Reading>> Handle(GetAllReadingsBySensorCodeQuery query, CancellationToken cancellationToken)
    {
        return await readingRepository.FindAllBySensorCodeAsNoTrackingAsync(query.SensorCode, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Reading>> Handle(GetAllReadingsByBatchCodeQuery query, CancellationToken cancellationToken)
    {
        return await readingRepository.FindAllByBatchCodeAsNoTrackingAsync(query.BatchCode, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Reading?> Handle(GetReadingByIdQuery query, CancellationToken cancellationToken)
    {
        return await readingRepository.FindByIdAsNoTrackingAsync(query.ReadingId, cancellationToken);
    }
}
