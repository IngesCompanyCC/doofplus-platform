using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="AuditEvent" />.
/// </summary>
public interface IAuditEventRepository : IBaseRepository<AuditEvent>
{
    /// <summary>
    ///     Gets the audit events with the record.
    /// </summary>
    Task<IEnumerable<AuditEvent>> FindAllByRecordAsNoTrackingAsync(string record, CancellationToken cancellationToken = default);
}
