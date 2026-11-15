namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;

/// <summary>
///     Query of the invitations, optionally of one organization.
/// </summary>
public record GetAllInvitationsQuery(int? OrganizationId = null);
