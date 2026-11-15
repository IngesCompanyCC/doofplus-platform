using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="CapaPlan" />.
/// </summary>
public interface ICapaPlanCommandService
{
    /// <summary>
    ///     Creates a capa plan.
    /// </summary>
    Task<Result<CapaPlan>> Handle(CreateCapaPlanCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Replaces the data of a capa plan.
    /// </summary>
    Task<Result<CapaPlan>> Handle(UpdateCapaPlanCommand command, CancellationToken cancellationToken);
}
