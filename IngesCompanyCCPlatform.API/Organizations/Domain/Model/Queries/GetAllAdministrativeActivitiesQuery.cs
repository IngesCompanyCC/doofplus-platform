namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

/// <summary>
///     Query to get the administrative activities, the most recent first.
/// </summary>
/// <param name="OrganizationId">The organization, or <c>null</c> for every activity.</param>
public record GetAllAdministrativeActivitiesQuery(int? OrganizationId);
