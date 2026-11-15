using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="Deviation" />.
/// </summary>
public interface IDeviationCommandService
{
    /// <summary>
    ///     Creates a deviation.
    /// </summary>
    Task<Result<Deviation>> Handle(CreateDeviationCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Replaces the data of a deviation.
    /// </summary>
    Task<Result<Deviation>> Handle(UpdateDeviationCommand command, CancellationToken cancellationToken);
}
