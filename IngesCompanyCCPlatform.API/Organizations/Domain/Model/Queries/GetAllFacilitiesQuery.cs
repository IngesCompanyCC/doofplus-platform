namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

/// <summary>
///     Query to get every facility, or only the ones of an organization.
/// </summary>
/// <param name="OrganizationId">The organization, or <c>null</c> for every facility.</param>
public record GetAllFacilitiesQuery(int? OrganizationId);
