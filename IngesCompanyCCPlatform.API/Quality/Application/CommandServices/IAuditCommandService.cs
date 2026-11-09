using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the audit command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAuditCommandService
{
    /// <summary>Schedules an audit.</summary>
    Task<Result<Audit>> Handle(ScheduleAuditCommand command, CancellationToken cancellationToken);

    /// <summary>Starts a scheduled audit.</summary>
    Task<Result<Audit>> Handle(StartAuditCommand command, CancellationToken cancellationToken);

    /// <summary>Records a finding or an observation.</summary>
    Task<Result<Audit>> Handle(RegisterAuditFindingCommand command, CancellationToken cancellationToken);

    /// <summary>Attaches the evidence package of an audit.</summary>
    Task<Result<Audit>> Handle(AttachAuditEvidencePackageCommand command, CancellationToken cancellationToken);

    /// <summary>Closes an audit.</summary>
    Task<Result<Audit>> Handle(CloseAuditCommand command, CancellationToken cancellationToken);
}
