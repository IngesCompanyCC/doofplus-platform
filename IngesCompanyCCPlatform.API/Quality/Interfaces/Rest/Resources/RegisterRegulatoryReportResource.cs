namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to register a regulatory report. Period: YYYY-MM.
/// </summary>
public record RegisterRegulatoryReportResource(int OrganizationId, string Period, string FileUrl);
