namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data that replaces a evidence.
/// </summary>
public record UpdateEvidenceResource(
    string? RecordCode,
    string? FileName,
    string? Detail,
    string? Status);
