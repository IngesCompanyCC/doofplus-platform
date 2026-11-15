using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a evidence.
/// </summary>
public record CreateEvidenceCommand(
    string RecordCode,
    string FileName,
    string Detail,
    EEvidenceStatus Status);
