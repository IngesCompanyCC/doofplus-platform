namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A regulatory report, as the API returns it.
/// </summary>
public record RegulatoryReportResource(
    int Id,
    string Code,
    string Title,
    string Description,
    string RegulatoryAuthority,
    string FileReference,
    DateOnly? DueDate,
    int? PreparedBy,
    string Status,
    DateTime? SubmittedAt);
