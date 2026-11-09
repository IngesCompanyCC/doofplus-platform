using System.Globalization;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the analytical result command service of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Applies the policy "when a result is out of specification, mark it OOS and register a deviation":
///     the result and its deviation are saved in the same unit of work.
/// </remarks>
public class AnalyticalResultCommandService(
    IAnalyticalResultRepository analyticalResultRepository,
    IDeviationRepository deviationRepository,
    IUnitOfWork unitOfWork) : IAnalyticalResultCommandService
{
    /// <inheritdoc />
    public async Task<Result<AnalyticalResult>> Handle(RegisterAnalyticalResultCommand command,
        CancellationToken cancellationToken)
    {
        var result = QualityDomainRules.Create(() => new AnalyticalResult(command));
        if (result.IsFailure) return result;

        var analyticalResult = result.Value!;
        await analyticalResultRepository.AddAsync(analyticalResult, cancellationToken);

        if (analyticalResult.OutOfSpecification)
            await deviationRepository.AddAsync(new Deviation(ToDeviationCommand(analyticalResult)), cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }

    private static RegisterDeviationCommand ToDeviationCommand(AnalyticalResult analyticalResult)
    {
        var specification = analyticalResult.Specification;
        var code = $"DEV-OOS-{analyticalResult.ProductionBatchId}-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
        var description = string.Create(CultureInfo.InvariantCulture,
            $"Out-of-specification result in {analyticalResult.TestName}: {analyticalResult.Value} {specification.Unit} " +
            $"(specification {specification.Min} to {specification.Max} {specification.Unit}).");
        return new RegisterDeviationCommand(code, analyticalResult.ProductionBatchId, EDeviationSeverity.Major,
            description);
    }
}
