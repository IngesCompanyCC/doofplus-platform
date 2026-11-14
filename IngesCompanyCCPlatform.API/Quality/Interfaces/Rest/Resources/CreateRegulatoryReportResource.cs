namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a regulatory report.
/// </summary>
public record CreateRegulatoryReportResource(
    string Code,
    string Title,
    string? Description,
    string? RegulatoryAuthority,
    string? FileReference,
    DateOnly? DueDate,
    int? PreparedBy);
