using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a regulatory report by its id.
/// </summary>
public record GetRegulatoryReportByIdQuery(RegulatoryReportId RegulatoryReportId);
