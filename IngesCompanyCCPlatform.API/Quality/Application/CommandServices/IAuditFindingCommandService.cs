using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="AuditFinding" />.
/// </summary>
public interface IAuditFindingCommandService
{
    /// <summary>
    ///     Creates a audit finding.
    /// </summary>
    Task<Result<AuditFinding>> Handle(CreateAuditFindingCommand command, CancellationToken cancellationToken);
}
