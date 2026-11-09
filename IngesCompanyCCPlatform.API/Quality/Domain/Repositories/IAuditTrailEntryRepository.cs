using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the audit trail repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAuditTrailEntryRepository : IBaseRepository<AuditTrailEntry>
{
    /// <summary>
    ///     Lists the audit trail of one record, newest first.
    /// </summary>
    /// <param name="entityType">The type of the record, for example <c>QualityDocument</c>.</param>
    /// <param name="entityId">The id of the record.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entries of the record.</returns>
    Task<IReadOnlyCollection<AuditTrailEntry>> FindByEntityAsync(string entityType, string entityId,
        CancellationToken cancellationToken = default);
}
