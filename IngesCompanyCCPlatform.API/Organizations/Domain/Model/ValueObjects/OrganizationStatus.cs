namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
///     Status of a registered organization.
/// </summary>
public enum OrganizationStatus
{
    PendingVerification = 0,
    Active = 1,
    Suspended = 2
}