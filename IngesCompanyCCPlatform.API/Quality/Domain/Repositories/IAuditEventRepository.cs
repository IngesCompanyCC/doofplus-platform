using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="AuditEvent" />.
/// </summary>
public interface IAuditEventRepository : IBaseRepository<AuditEvent>
{
    /// <summary>
    ///     Gets the audit events filtered by aggregate type and aggregate id.
    /// </summary>
    Task<IEnumerable<AuditEvent>> FindAllByAggregateTypeAggregateIdAsNoTrackingAsync(string? aggregateType, int? aggregateId, CancellationToken cancellationToken = default);
}
