using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the audit command service of the Quality &amp; Compliance bounded context.
/// </summary>
public class AuditCommandService(IAuditRepository auditRepository, IUnitOfWork unitOfWork) : IAuditCommandService
{
    /// <inheritdoc />
    public async Task<Result<Audit>> Handle(ScheduleAuditCommand command, CancellationToken cancellationToken)
    {
        var audit = new Audit(command);
        await auditRepository.AddAsync(audit, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Audit>.Success(audit);
    }

    /// <inheritdoc />
    public Task<Result<Audit>> Handle(StartAuditCommand command, CancellationToken cancellationToken)
    {
        return Update(command.AuditId, audit => audit.Start(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Audit>> Handle(RegisterAuditFindingCommand command, CancellationToken cancellationToken)
    {
        return Update(command.AuditId, audit => audit.RegisterFinding(command.Type, command.Description),
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Audit>> Handle(AttachAuditEvidencePackageCommand command, CancellationToken cancellationToken)
    {
        return Update(command.AuditId, audit => audit.AttachEvidencePackage(command.FileUrl), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Audit>> Handle(CloseAuditCommand command, CancellationToken cancellationToken)
    {
        return Update(command.AuditId, audit => audit.Close(), cancellationToken);
    }

    private async Task<Result<Audit>> Update(AuditId id, Action<Audit> behavior, CancellationToken cancellationToken)
    {
        var audit = await auditRepository.FindByIdAsync(id, cancellationToken);
        if (audit is null)
            return Result<Audit>.Failure(QualityErrors.AuditNotFound(id), QualityError.AuditNotFound);

        var result = QualityDomainRules.Apply(audit, behavior);
        if (result.IsFailure) return result;

        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }
}
