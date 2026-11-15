using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Evidence of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class Evidence : AggregateRoot
{
    private const string RecordCodeNotBlankMessageKey = "quality.evidence.error.recordCode.notBlank";
    private const string FileNameNotBlankMessageKey = "quality.evidence.error.fileName.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Evidence()
    {
        RecordCode = string.Empty;
        FileName = string.Empty;
        Detail = string.Empty;
    }

    /// <summary>
    ///     Creates the evidence from its command.
    /// </summary>
    /// <param name="command">The data of the evidence.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public Evidence(CreateEvidenceCommand command) : this() => Apply(command.RecordCode, command.FileName, command.Detail, command.Status);

    /// <summary>
    ///     The id of the evidence.
    /// </summary>
    public EvidenceId Id { get; }

    /// <summary>
    ///     The record code.
    /// </summary>
    public string RecordCode { get; private set; }

    /// <summary>
    ///     The file name.
    /// </summary>
    public string FileName { get; private set; }

    /// <summary>
    ///     The detail.
    /// </summary>
    public string Detail { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EEvidenceStatus Status { get; private set; }

    /// <summary>
    ///     Replaces the data of the evidence with the data of the command.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public void Update(UpdateEvidenceCommand command) => Apply(command.RecordCode, command.FileName, command.Detail, command.Status);

    /// <summary>
    ///     Reviewed evidence cannot be removed, so the investigation keeps its integrity.
    /// </summary>
    public bool CanBeRemoved() => Status != EEvidenceStatus.Reviewed;

    private void Apply(string recordCode, string fileName, string detail, EEvidenceStatus status)
    {
        if (string.IsNullOrWhiteSpace(recordCode)) throw new ArgumentException(RecordCodeNotBlankMessageKey);
        RecordCode = recordCode?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException(FileNameNotBlankMessageKey);
        FileName = fileName?.Trim() ?? string.Empty;
        Detail = detail?.Trim() ?? string.Empty;
        Status = status;
    }
}
