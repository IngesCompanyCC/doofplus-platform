using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="RegulatoryReport" />.
/// </summary>
public interface IRegulatoryReportCommandService
{
    /// <summary>
    ///     Creates a regulatory report.
    /// </summary>
    Task<Result<RegulatoryReport>> Handle(CreateRegulatoryReportCommand command, CancellationToken cancellationToken);
}
