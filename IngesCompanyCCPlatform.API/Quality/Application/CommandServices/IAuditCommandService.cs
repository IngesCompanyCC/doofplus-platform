using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="Audit" />.
/// </summary>
public interface IAuditCommandService
{
    /// <summary>
    ///     Creates a audit.
    /// </summary>
    Task<Result<Audit>> Handle(CreateAuditCommand command, CancellationToken cancellationToken);
}
