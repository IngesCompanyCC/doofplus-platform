using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="AuditEvent" />.
/// </summary>
public interface IAuditEventCommandService
{
    /// <summary>
    ///     Creates a audit event.
    /// </summary>
    Task<Result<AuditEvent>> Handle(CreateAuditEventCommand command, CancellationToken cancellationToken);
}
