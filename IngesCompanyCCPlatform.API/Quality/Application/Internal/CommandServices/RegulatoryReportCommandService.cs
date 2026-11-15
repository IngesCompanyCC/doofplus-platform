using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Command service of <see cref="RegulatoryReport" />.
/// </summary>
public class RegulatoryReportCommandService(
    IRegulatoryReportRepository regulatoryReportRepository,
    IUnitOfWork unitOfWork) : IRegulatoryReportCommandService
{
    /// <inheritdoc />
    public async Task<Result<RegulatoryReport>> Handle(CreateRegulatoryReportCommand command, CancellationToken cancellationToken)
    {
        if (await regulatoryReportRepository.ExistsByCodeAsync(command.Code.Trim(), cancellationToken))
            return Result<RegulatoryReport>.Failure(RegulatoryReportErrors.DuplicateCode, QualityError.DuplicateRegulatoryReportCode);

        var regulatoryReport = new RegulatoryReport(command);
        await regulatoryReportRepository.AddAsync(regulatoryReport, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<RegulatoryReport>.Success(regulatoryReport);
    }
}
