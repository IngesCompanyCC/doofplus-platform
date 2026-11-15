namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a audit. Missing optional values take their default.
/// </summary>
public record CreateAuditResource(
    string? Code,
    string? Title,
    string? Period,
    int? AreasReviewed,
    int? AreasTotal,
    string? Status);
