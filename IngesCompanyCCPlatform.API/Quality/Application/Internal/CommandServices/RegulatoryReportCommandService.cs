using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the regulatory report command service of the Quality &amp; Compliance bounded context.
/// </summary>
public class RegulatoryReportCommandService(
    IRegulatoryReportRepository regulatoryReportRepository,
    IUnitOfWork unitOfWork) : IRegulatoryReportCommandService
{
    /// <inheritdoc />
    public async Task<Result<RegulatoryReport>> Handle(RegisterRegulatoryReportCommand command,
        CancellationToken cancellationToken)
    {
        var result = QualityDomainRules.Create(() => new RegulatoryReport(command));
        if (result.IsFailure) return result;

        await regulatoryReportRepository.AddAsync(result.Value!, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }
}
