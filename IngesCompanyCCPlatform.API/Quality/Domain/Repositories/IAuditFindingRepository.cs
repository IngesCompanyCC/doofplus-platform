using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="AuditFinding" />.
/// </summary>
public interface IAuditFindingRepository : IBaseRepository<AuditFinding>
{
    /// <summary>
    ///     Gets the audit findings with the audit code.
    /// </summary>
    Task<IEnumerable<AuditFinding>> FindAllByAuditCodeAsNoTrackingAsync(string auditCode, CancellationToken cancellationToken = default);
}
