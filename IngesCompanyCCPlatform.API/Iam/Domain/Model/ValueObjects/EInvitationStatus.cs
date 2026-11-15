namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;

/// <summary>
///     Status of an invitation: it waits until the person accepts it or it expires.
/// </summary>
public enum EInvitationStatus
{
    Pending,
    Accepted,
    Expired
}
