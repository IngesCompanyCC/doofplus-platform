using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the regulatory reports of an organization.
/// </summary>
public record GetRegulatoryReportsByOrganizationIdQuery(OrganizationId OrganizationId);
