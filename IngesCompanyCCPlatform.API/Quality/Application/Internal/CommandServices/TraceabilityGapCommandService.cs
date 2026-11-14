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
///     Command service of <see cref="TraceabilityGap" />.
/// </summary>
public class TraceabilityGapCommandService(
    ITraceabilityGapRepository traceabilityGapRepository,
    IUnitOfWork unitOfWork) : ITraceabilityGapCommandService
{
    /// <inheritdoc />
    public async Task<Result<TraceabilityGap>> Handle(CreateTraceabilityGapCommand command, CancellationToken cancellationToken)
    {
        var traceabilityGap = new TraceabilityGap(command);
        await traceabilityGapRepository.AddAsync(traceabilityGap, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<TraceabilityGap>.Success(traceabilityGap);
    }
}
