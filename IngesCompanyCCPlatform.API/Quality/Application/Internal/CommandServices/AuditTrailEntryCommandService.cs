using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the audit trail command service of the Quality &amp; Compliance bounded context.
/// </summary>
public class AuditTrailEntryCommandService(
    IAuditTrailEntryRepository auditTrailEntryRepository,
    IUnitOfWork unitOfWork) : IAuditTrailEntryCommandService
{
    /// <inheritdoc />
    public async Task<Result<AuditTrailEntry>> Handle(RecordAuditTrailEntryCommand command,
        CancellationToken cancellationToken)
    {
        var result = QualityDomainRules.Create(() => new AuditTrailEntry(command));
        if (result.IsFailure) return result;

        await auditTrailEntryRepository.AddAsync(result.Value!, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }
}
