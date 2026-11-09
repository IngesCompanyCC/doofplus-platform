using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to register a monthly regulatory report generated for an organization.
/// </summary>
public record RegisterRegulatoryReportCommand(OrganizationId OrganizationId, ReportPeriod Period, string FileUrl);
