namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

/// <summary>
///     An entry of the recent administrative activity.
/// </summary>
public record AdministrativeActivityResource(int Id, int OrganizationId, string Title, string Actor, DateTime OccurredAt);
