using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="TraceabilityGap" />.
/// </summary>
public interface ITraceabilityGapCommandService
{
    /// <summary>
    ///     Creates a traceability gap.
    /// </summary>
    Task<Result<TraceabilityGap>> Handle(CreateTraceabilityGapCommand command, CancellationToken cancellationToken);
}
