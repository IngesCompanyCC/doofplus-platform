using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Analytical result of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class AnalyticalResult : AggregateRoot
{
    private const string BatchCodeNotBlankMessageKey = "quality.analyticalResult.error.batchCode.notBlank";
    private const string TestNotBlankMessageKey = "quality.analyticalResult.error.test.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected AnalyticalResult()
    {
        BatchCode = string.Empty;
        SampleCode = string.Empty;
        ReportCode = string.Empty;
        Test = string.Empty;
        Protocol = string.Empty;
        Result = string.Empty;
        Specification = string.Empty;
        Analyst = string.Empty;
    }

    /// <summary>
    ///     Creates the analytical result from its command.
    /// </summary>
    /// <param name="command">The data of the analytical result.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public AnalyticalResult(CreateAnalyticalResultCommand command) : this() => Apply(command.BatchCode, command.SampleCode, command.ReportCode, command.Test, command.Protocol, command.Result, command.Specification, command.Analyst, command.Status, command.Approval);

    /// <summary>
    ///     The id of the analytical result.
    /// </summary>
    public AnalyticalResultId Id { get; }

    /// <summary>
    ///     The batch code.
    /// </summary>
    public string BatchCode { get; private set; }

    /// <summary>
    ///     The sample code.
    /// </summary>
    public string SampleCode { get; private set; }

    /// <summary>
    ///     The report code.
    /// </summary>
    public string ReportCode { get; private set; }

    /// <summary>
    ///     The test.
    /// </summary>
    public string Test { get; private set; }

    /// <summary>
    ///     The protocol.
    /// </summary>
    public string Protocol { get; private set; }

    /// <summary>
    ///     The result.
    /// </summary>
    public string Result { get; private set; }

    /// <summary>
    ///     The specification.
    /// </summary>
    public string Specification { get; private set; }

    /// <summary>
    ///     The analyst.
    /// </summary>
    public string Analyst { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EResultStatus Status { get; private set; }

    /// <summary>
    ///     The approval.
    /// </summary>
    public EApprovalState Approval { get; private set; }

    /// <summary>
    ///     Replaces the data of the analytical result with the data of the command.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public void Update(UpdateAnalyticalResultCommand command) => Apply(command.BatchCode, command.SampleCode, command.ReportCode, command.Test, command.Protocol, command.Result, command.Specification, command.Analyst, command.Status, command.Approval);

    private void Apply(string batchCode, string sampleCode, string reportCode, string test, string protocol, string result, string specification, string analyst, EResultStatus status, EApprovalState approval)
    {
        if (string.IsNullOrWhiteSpace(batchCode)) throw new ArgumentException(BatchCodeNotBlankMessageKey);
        BatchCode = batchCode?.Trim() ?? string.Empty;
        SampleCode = sampleCode?.Trim() ?? string.Empty;
        ReportCode = reportCode?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(test)) throw new ArgumentException(TestNotBlankMessageKey);
        Test = test?.Trim() ?? string.Empty;
        Protocol = protocol?.Trim() ?? string.Empty;
        Result = result?.Trim() ?? string.Empty;
        Specification = specification?.Trim() ?? string.Empty;
        Analyst = analyst?.Trim() ?? string.Empty;
        Status = status;
        Approval = approval;
    }
}
