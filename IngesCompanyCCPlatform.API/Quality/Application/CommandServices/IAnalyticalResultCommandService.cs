using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the analytical result command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAnalyticalResultCommandService
{
    /// <summary>
    ///     Registers an analytical result. An out-of-specification result also registers its deviation.
    /// </summary>
    Task<Result<AnalyticalResult>> Handle(RegisterAnalyticalResultCommand command, CancellationToken cancellationToken);
}
