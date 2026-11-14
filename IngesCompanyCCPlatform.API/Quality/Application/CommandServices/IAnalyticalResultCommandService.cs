using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="AnalyticalResult" />.
/// </summary>
public interface IAnalyticalResultCommandService
{
    /// <summary>
    ///     Creates a analytical result.
    /// </summary>
    Task<Result<AnalyticalResult>> Handle(CreateAnalyticalResultCommand command, CancellationToken cancellationToken);
}
