using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the AnalyticalResult aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Keeps the value of a laboratory test of a production batch and detects whether it is out of
///     specification (OOS). An OOS result blocks the release of the batch until its deviation is closed.
/// </remarks>
public partial class AnalyticalResult : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AnalyticalResult" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected AnalyticalResult()
    {
        TestName = string.Empty;
        Specification = null!;
    }

    /// <summary>
    ///     Initializes a new analytical result and checks it against its specification.
    /// </summary>
    /// <param name="command">The <see cref="RegisterAnalyticalResultCommand" /> with the test data.</param>
    public AnalyticalResult(RegisterAnalyticalResultCommand command) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.TestName);
        ProductionBatchId = command.ProductionBatchId;
        TestName = command.TestName;
        Specification = command.Specification;
        AnalystId = command.AnalystId;
        Calculate(command.Value);
    }

    public AnalyticalResultId Id { get; }
    public ProductionBatchId ProductionBatchId { get; private set; }
    public string TestName { get; private set; }
    public decimal Value { get; private set; }
    public SpecificationRange Specification { get; private set; }

    /// <summary>
    ///     Gets whether the value is out of specification (OOS).
    /// </summary>
    public bool OutOfSpecification { get; private set; }

    public UserId AnalystId { get; private set; }

    /// <summary>
    ///     Sets the value of the test and checks it against the specification.
    /// </summary>
    /// <param name="value">The value obtained by the analyst.</param>
    public void Calculate(decimal value)
    {
        Value = value;
        OutOfSpecification = !Specification.Contains(value);
    }
}
