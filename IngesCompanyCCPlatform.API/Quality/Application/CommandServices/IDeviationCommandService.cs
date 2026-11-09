using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the deviation command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IDeviationCommandService
{
    /// <summary>Registers an open deviation.</summary>
    Task<Result<Deviation>> Handle(RegisterDeviationCommand command, CancellationToken cancellationToken);

    /// <summary>Changes the severity of a deviation.</summary>
    Task<Result<Deviation>> Handle(ClassifyDeviationCommand command, CancellationToken cancellationToken);

    /// <summary>Starts the investigation of a deviation.</summary>
    Task<Result<Deviation>> Handle(StartDeviationInvestigationCommand command, CancellationToken cancellationToken);

    /// <summary>Registers the root cause of a deviation.</summary>
    Task<Result<Deviation>> Handle(RegisterRootCauseCommand command, CancellationToken cancellationToken);

    /// <summary>Adds a CAPA action to a deviation.</summary>
    Task<Result<Deviation>> Handle(AddCapaActionCommand command, CancellationToken cancellationToken);

    /// <summary>Updates the progress of a CAPA action.</summary>
    Task<Result<Deviation>> Handle(UpdateCapaActionProgressCommand command, CancellationToken cancellationToken);

    /// <summary>Verifies a CAPA action.</summary>
    Task<Result<Deviation>> Handle(VerifyCapaActionCommand command, CancellationToken cancellationToken);

    /// <summary>Closes a deviation.</summary>
    Task<Result<Deviation>> Handle(CloseDeviationCommand command, CancellationToken cancellationToken);
}
