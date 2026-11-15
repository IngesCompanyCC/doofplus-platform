using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;

/// <summary>
///     Invitation of a new member of the laboratory, as in <c>class-01-iam</c>: it offers a role in a facility and
///     waits until the person accepts it or it expires.
/// </summary>
public partial class Invitation
{
    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    private Invitation()
    {
    }

    /// <summary>
    ///     Sends the invitation: it starts pending.
    /// </summary>
    /// <param name="command">The data of the invitation.</param>
    /// <param name="role">The role offered, already validated.</param>
    public Invitation(SendInvitationCommand command, Roles role)
    {
        OrganizationId = command.OrganizationId;
        Facility = command.Facility.Trim();
        Role = role;
        FullName = command.FullName.Trim();
        Email = command.Email.Trim().ToLowerInvariant();
        Note = command.Note?.Trim() ?? string.Empty;
        Status = EInvitationStatus.Pending;
        SentAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     The id of the invitation.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     The organization that invites.
    /// </summary>
    public int OrganizationId { get; private set; }

    /// <summary>
    ///     The facility the person will work in.
    /// </summary>
    public string Facility { get; private set; } = string.Empty;

    /// <summary>
    ///     The role offered.
    /// </summary>
    public Roles Role { get; private set; }

    /// <summary>
    ///     The full name of the person.
    /// </summary>
    public string FullName { get; private set; } = string.Empty;

    /// <summary>
    ///     The work email that receives the invitation.
    /// </summary>
    public string Email { get; private set; } = string.Empty;

    /// <summary>
    ///     An optional note for the person.
    /// </summary>
    public string Note { get; private set; } = string.Empty;

    /// <summary>
    ///     Whether the invitation is pending, accepted or expired.
    /// </summary>
    public EInvitationStatus Status { get; private set; }

    /// <summary>
    ///     When the invitation was sent (UTC).
    /// </summary>
    public DateTime SentAt { get; private set; }

    /// <summary>
    ///     Accepts a pending invitation.
    /// </summary>
    /// <returns>False when the invitation is not pending.</returns>
    public bool Accept()
    {
        if (Status != EInvitationStatus.Pending) return false;
        Status = EInvitationStatus.Accepted;
        return true;
    }

    /// <summary>
    ///     Expires a pending invitation.
    /// </summary>
    /// <returns>False when the invitation is not pending.</returns>
    public bool Expire()
    {
        if (Status != EInvitationStatus.Pending) return false;
        Status = EInvitationStatus.Expired;
        return true;
    }
}
