using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A missing link in the genealogy of a production batch.
/// </summary>
public partial class TraceabilityGap : AggregateRoot
{
    private const string DescriptionNotBlankMessageKey = "quality.traceabilityGap.error.description.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected TraceabilityGap()
    {
        Description = string.Empty;
        MissingReference = string.Empty;
        AffectedProcess = string.Empty;
    }

    /// <summary>
    ///     Creates the traceability gap from its command.
    /// </summary>
    /// <param name="command">The data of the traceability gap.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public TraceabilityGap(CreateTraceabilityGapCommand command) : this()
    {
        BatchId = command.BatchId;
        if (string.IsNullOrWhiteSpace(command.Description)) throw new ArgumentException(DescriptionNotBlankMessageKey);
        Description = command.Description.Trim();
        MissingReference = command.MissingReference?.Trim() ?? string.Empty;
        AffectedProcess = command.AffectedProcess?.Trim() ?? string.Empty;
        Status = ETraceabilityGapStatus.Open;
        DetectedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     The id of the traceability gap.
    /// </summary>
    public TraceabilityGapId Id { get; }

    /// <summary>
    ///     The id of the production batch.
    /// </summary>
    public int BatchId { get; private set; }
    /// <summary>
    ///     What is missing.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     The reference that is missing, for example a lot code.
    /// </summary>
    public string MissingReference { get; private set; }
    /// <summary>
    ///     The affected process.
    /// </summary>
    public string AffectedProcess { get; private set; }
    /// <summary>
    ///     Where the gap is.
    /// </summary>
    public ETraceabilityGapStatus Status { get; private set; }
    /// <summary>
    ///     The id of the user who resolved it.
    /// </summary>
    public int? ReviewedBy { get; private set; }
    /// <summary>
    ///     When the gap was registered (UTC).
    /// </summary>
    public DateTime DetectedAt { get; private set; }
    /// <summary>
    ///     When the gap was resolved (UTC).
    /// </summary>
    public DateTime? ResolvedAt { get; private set; }

    /// <summary>
    ///     Starts the review of an open gap.
    /// </summary>
    public bool StartReview()
    {
        if (Status != ETraceabilityGapStatus.Open) return false;
        Status = ETraceabilityGapStatus.UnderReview;
        return true;
    }

    /// <summary>
    ///     Resolves a gap under review.
    /// </summary>
    public bool Resolve(int reviewerId)
    {
        if (Status != ETraceabilityGapStatus.UnderReview) return false;
        Status = ETraceabilityGapStatus.Resolved;
        ReviewedBy = reviewerId;
        ResolvedAt = DateTime.UtcNow;
        return true;
    }
}
