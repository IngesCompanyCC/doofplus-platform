using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to replace the data of a evidence.
/// </summary>
public record UpdateEvidenceCommand(
    int EvidenceId,
    string RecordCode,
    string FileName,
    string Detail,
    EEvidenceStatus Status);
