using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="Evidence" />.
/// </summary>
public interface IEvidenceCommandService
{
    /// <summary>
    ///     Creates a evidence.
    /// </summary>
    Task<Result<Evidence>> Handle(CreateEvidenceCommand command, CancellationToken cancellationToken);
}
