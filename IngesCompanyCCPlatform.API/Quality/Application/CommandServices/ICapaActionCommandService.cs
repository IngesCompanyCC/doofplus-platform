using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="CapaAction" />.
/// </summary>
public interface ICapaActionCommandService
{
    /// <summary>
    ///     Creates a capa action.
    /// </summary>
    Task<Result<CapaAction>> Handle(CreateCapaActionCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Replaces the data of a capa action.
    /// </summary>
    Task<Result<CapaAction>> Handle(UpdateCapaActionCommand command, CancellationToken cancellationToken);
}
