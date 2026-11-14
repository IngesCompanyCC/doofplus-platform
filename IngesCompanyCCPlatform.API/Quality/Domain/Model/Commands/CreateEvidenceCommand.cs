using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a evidence.
/// </summary>
public record CreateEvidenceCommand(
    string Title,
    string Description,
    EEvidenceType Type,
    string FileReference,
    int? DeviationId,
    int? DocumentId,
    int? UploadedBy);
