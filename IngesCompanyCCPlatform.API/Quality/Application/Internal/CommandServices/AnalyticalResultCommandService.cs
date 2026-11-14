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
///     Command service of <see cref="AnalyticalResult" />.
/// </summary>
public class AnalyticalResultCommandService(
    IAnalyticalResultRepository analyticalResultRepository,
    IUnitOfWork unitOfWork) : IAnalyticalResultCommandService
{
    /// <inheritdoc />
    public async Task<Result<AnalyticalResult>> Handle(CreateAnalyticalResultCommand command, CancellationToken cancellationToken)
    {
        var analyticalResult = new AnalyticalResult(command);
        await analyticalResultRepository.AddAsync(analyticalResult, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<AnalyticalResult>.Success(analyticalResult);
    }
}
