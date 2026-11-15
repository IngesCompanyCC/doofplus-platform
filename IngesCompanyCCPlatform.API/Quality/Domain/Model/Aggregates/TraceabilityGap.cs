using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Traceability gap of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class TraceabilityGap : AggregateRoot
{
    private const string BatchCodeNotBlankMessageKey = "quality.traceabilityGap.error.batchCode.notBlank";
    private const string MissingRecordNotBlankMessageKey = "quality.traceabilityGap.error.missingRecord.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected TraceabilityGap()
    {
        BatchCode = string.Empty;
        Product = string.Empty;
        MissingRecord = string.Empty;
        Detail = string.Empty;
    }

    /// <summary>
    ///     Creates the traceability gap from its command.
    /// </summary>
    /// <param name="command">The data of the traceability gap.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public TraceabilityGap(CreateTraceabilityGapCommand command) : this() => Apply(command.BatchCode, command.Product, command.MissingRecord, command.Detail, command.Status);

    /// <summary>
    ///     The id of the traceability gap.
    /// </summary>
    public TraceabilityGapId Id { get; }

    /// <summary>
    ///     The batch code.
    /// </summary>
    public string BatchCode { get; private set; }

    /// <summary>
    ///     The product.
    /// </summary>
    public string Product { get; private set; }

    /// <summary>
    ///     The missing record.
    /// </summary>
    public string MissingRecord { get; private set; }

    /// <summary>
    ///     The detail.
    /// </summary>
    public string Detail { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EGapStatus Status { get; private set; }

    private void Apply(string batchCode, string product, string missingRecord, string detail, EGapStatus status)
    {
        if (string.IsNullOrWhiteSpace(batchCode)) throw new ArgumentException(BatchCodeNotBlankMessageKey);
        BatchCode = batchCode?.Trim() ?? string.Empty;
        Product = product?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(missingRecord)) throw new ArgumentException(MissingRecordNotBlankMessageKey);
        MissingRecord = missingRecord?.Trim() ?? string.Empty;
        Detail = detail?.Trim() ?? string.Empty;
        Status = status;
    }
}
