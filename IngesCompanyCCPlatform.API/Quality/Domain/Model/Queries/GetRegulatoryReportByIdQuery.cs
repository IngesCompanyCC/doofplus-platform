namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a regulatory report by its id.
/// </summary>
/// <param name="RegulatoryReportId">The id of the regulatory report.</param>
public record GetRegulatoryReportByIdQuery(int RegulatoryReportId);
