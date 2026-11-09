using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the audit trail command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAuditTrailEntryCommandService
{
    /// <summary>Records a change in the audit trail.</summary>
    Task<Result<AuditTrailEntry>> Handle(RecordAuditTrailEntryCommand command, CancellationToken cancellationToken);
}
