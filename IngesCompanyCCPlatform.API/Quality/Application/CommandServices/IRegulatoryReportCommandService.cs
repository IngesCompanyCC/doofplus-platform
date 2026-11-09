using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the regulatory report command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IRegulatoryReportCommandService
{
    /// <summary>Registers a monthly regulatory report.</summary>
    Task<Result<RegulatoryReport>> Handle(RegisterRegulatoryReportCommand command, CancellationToken cancellationToken);
}
