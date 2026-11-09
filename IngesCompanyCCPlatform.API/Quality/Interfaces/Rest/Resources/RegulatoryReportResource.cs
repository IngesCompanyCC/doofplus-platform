namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing a regulatory report.
/// </summary>
public record RegulatoryReportResource(int Id, int OrganizationId, string Period, string FileUrl, DateTime GeneratedAt);
