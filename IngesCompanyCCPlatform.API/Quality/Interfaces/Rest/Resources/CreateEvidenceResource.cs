namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a evidence. Missing optional values take their default.
/// </summary>
public record CreateEvidenceResource(
    string? RecordCode,
    string? FileName,
    string? Detail,
    string? Status);
