using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     The result of a laboratory test of a production batch.
/// </summary>
public partial class AnalyticalResult : AggregateRoot
{
    private const string TestNameNotBlankMessageKey = "quality.analyticalResult.error.testName.notBlank";
    private const string RangeInvalidMessageKey = "quality.analyticalResult.error.minimumValue.range";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected AnalyticalResult()
    {
        TestName = string.Empty;
        Parameter = string.Empty;
        Unit = string.Empty;
    }

    /// <summary>
    ///     Creates the analytical result from its command.
    /// </summary>
    /// <param name="command">The data of the analytical result.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public AnalyticalResult(CreateAnalyticalResultCommand command) : this()
    {
        BatchId = command.BatchId;
        if (string.IsNullOrWhiteSpace(command.TestName)) throw new ArgumentException(TestNameNotBlankMessageKey);
        TestName = command.TestName.Trim();
        Parameter = command.Parameter?.Trim() ?? string.Empty;
        MeasuredValue = command.MeasuredValue;
        MinimumValue = command.MinimumValue;
        MaximumValue = command.MaximumValue;
        Unit = command.Unit?.Trim() ?? string.Empty;
        Status = EAnalyticalResultStatus.Pending;
        TestedAt = command.TestedAt;
        if (command.MinimumValue > command.MaximumValue) throw new ArgumentException(RangeInvalidMessageKey);
        Status = command.MeasuredValue >= command.MinimumValue && command.MeasuredValue <= command.MaximumValue
            ? EAnalyticalResultStatus.WithinSpecification
            : EAnalyticalResultStatus.OutOfSpecification;

    }

    /// <summary>
    ///     The id of the analytical result.
    /// </summary>
    public AnalyticalResultId Id { get; }

    /// <summary>
    ///     The id of the tested production batch.
    /// </summary>
    public int BatchId { get; private set; }
    /// <summary>
    ///     The name of the test, for example Assay.
    /// </summary>
    public string TestName { get; private set; }
    /// <summary>
    ///     The measured parameter.
    /// </summary>
    public string Parameter { get; private set; }
    /// <summary>
    ///     The measured value.
    /// </summary>
    public decimal MeasuredValue { get; private set; }
    /// <summary>
    ///     The lowest accepted value.
    /// </summary>
    public decimal MinimumValue { get; private set; }
    /// <summary>
    ///     The highest accepted value.
    /// </summary>
    public decimal MaximumValue { get; private set; }
    /// <summary>
    ///     The unit of the values.
    /// </summary>
    public string Unit { get; private set; }
    /// <summary>
    ///     Where the result is in its review.
    /// </summary>
    public EAnalyticalResultStatus Status { get; private set; }
    /// <summary>
    ///     The id of the user who reviewed the result.
    /// </summary>
    public int? ReviewedBy { get; private set; }
    /// <summary>
    ///     When the test was done (UTC).
    /// </summary>
    public DateTime? TestedAt { get; private set; }
    /// <summary>
    ///     When the result was reviewed (UTC).
    /// </summary>
    public DateTime? ReviewedAt { get; private set; }

    /// <summary>
    ///     Approves a result within specification.
    /// </summary>
    public bool Approve(int reviewerId)
    {
        if (Status != EAnalyticalResultStatus.WithinSpecification) return false;
        Status = EAnalyticalResultStatus.Approved;
        ReviewedBy = reviewerId;
        ReviewedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Rejects a result that is not approved.
    /// </summary>
    public bool Reject(int reviewerId)
    {
        if (Status == EAnalyticalResultStatus.Approved) return false;
        Status = EAnalyticalResultStatus.Rejected;
        ReviewedBy = reviewerId;
        ReviewedAt = DateTime.UtcNow;
        return true;
    }
}
