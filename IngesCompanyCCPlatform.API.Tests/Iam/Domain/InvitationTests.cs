using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Iam.Domain;

/// <summary>
///     Tests of <see cref="Invitation" />.
/// </summary>
public class InvitationTests
{
    private static Invitation Sent() => new(new SendInvitationCommand(1, " Ate manufacturing ", "internal-auditor",
        " Óscar Ramos ", " Oscar.Ramos@Andinos.com.pe ", null), Roles.RoleInternalAuditor);

    [Fact]
    public void ANewInvitationShouldBePendingWithItsDataTrimmed()
    {
        var invitation = Sent();

        Assert.Equal(EInvitationStatus.Pending, invitation.Status);
        Assert.Equal("oscar.ramos@andinos.com.pe", invitation.Email);
        Assert.Equal("Óscar Ramos", invitation.FullName);
        Assert.Equal("Ate manufacturing", invitation.Facility);
        Assert.Equal(string.Empty, invitation.Note);
    }

    [Fact]
    public void OnlyAPendingInvitationShouldBeAcceptedOrExpired()
    {
        var invitation = Sent();

        Assert.True(invitation.Accept());
        Assert.False(invitation.Expire());
        Assert.Equal(EInvitationStatus.Accepted, invitation.Status);
    }
}
