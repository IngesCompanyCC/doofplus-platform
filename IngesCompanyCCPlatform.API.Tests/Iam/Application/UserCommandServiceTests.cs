using IngesCompanyCCPlatform.API.Iam.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Application.Internal.OutboundServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Tests.Iam.Application;

/// <summary>
///     Tests of the sign-in with two-factor authentication and the sign-up of <see cref="UserCommandService" />,
///     with in-memory test doubles.
/// </summary>
public class UserCommandServiceTests
{
    private readonly InMemoryUserRepository _users = new();
    private readonly InMemoryInvitationRepository _invitations = new();
    private readonly UserCommandService _service;

    public UserCommandServiceTests()
    {
        _service = new UserCommandService(_users, _invitations, new FakeTokenService(), new FakeHashingService(),
            new FakeUnitOfWork());
    }

    private static SignUpCommand SignUp(string email = "elena.paz@andinos.com.pe", string password = "Secret123",
        string? role = "qa-specialist", string environment = "qa", string? twoFactorCode = null,
        int organizationId = 1, string? privilege = null) =>
        new(email, password, "Elena Paz", role, environment, "Lima", null, privilege, organizationId,
            "Laboratorios Andinos", "Ate plant", twoFactorCode);

    private Invitation Invite(string email, string role, int organizationId = 1, string facility = "Ate plant")
    {
        Assert.True(RolesExtensions.TryFromKey(role, out var parsed));
        var invitation = new Invitation(
            new SendInvitationCommand(organizationId, facility, role, "Luis Rojas", email, null), parsed);
        _invitations.Items.Add(invitation);
        return invitation;
    }

    [Fact]
    public async Task SignUpHashesThePasswordAndTheCode()
    {
        var result = await _service.Handle(SignUp(email: " Elena.Paz@andinos.com.pe ", twoFactorCode: "123456"));

        Assert.True(result.IsSuccess);
        var user = Assert.Single(_users.Items);
        Assert.Equal("elena.paz@andinos.com.pe", user.Email);
        Assert.Equal("hash:Secret123", user.PasswordHash);
        Assert.Equal("hash:123456", user.TwoFactorCodeHash);
        Assert.Equal("EP", user.Initials);
        Assert.Equal("active", user.Status);
    }

    [Theory]
    [InlineData("elena", "Secret123", "qa-specialist", "qa")]
    [InlineData("elena.paz@andinos.com.pe", "short", "qa-specialist", "qa")]
    [InlineData("elena.paz@andinos.com.pe", "Secret123", "chef", "qa")]
    [InlineData("elena.paz@andinos.com.pe", "Secret123", "qa-specialist", "kitchen")]
    public async Task SignUpRejectsInvalidData(string email, string password, string role, string environment)
    {
        var result = await _service.Handle(SignUp(email, password, role, environment));

        Assert.True(result.IsFailure);
        Assert.Equal(IamError.InvalidUserData, result.Error);
    }

    [Fact]
    public async Task SignUpRejectsARepeatedEmail()
    {
        await _service.Handle(SignUp());
        var result = await _service.Handle(SignUp(email: "ELENA.PAZ@andinos.com.pe"));

        Assert.Equal(IamError.EmailAlreadyTaken, result.Error);
    }

    [Fact]
    public async Task TheFirstUserOfAnOrganizationIsItsAdministratorWhateverRoleItAsks()
    {
        var result = await _service.Handle(SignUp(role: "qa-specialist", environment: "qa"));

        Assert.True(result.IsSuccess);
        var user = Assert.Single(_users.Items);
        Assert.Equal(Roles.RoleAdministrator, user.Role);
        Assert.Equal("administrator", user.Privilege);
        Assert.Equal("administration", user.Environment);
    }

    [Fact]
    public async Task AnotherUserWithoutAnInvitationCannotJoinTheOrganization()
    {
        await _service.Handle(SignUp());

        var result = await _service.Handle(SignUp(email: "intruder@mail.com", role: "administrator",
            environment: "administration", privilege: "administrator"));

        Assert.Equal(IamError.InvitationRequired, result.Error);
        Assert.Single(_users.Items);
    }

    [Fact]
    public async Task AnInvitedUserTakesTheRoleAndTheOrganizationOfTheInvitation()
    {
        await _service.Handle(SignUp());
        var invitation = Invite("luis.rojas@andinos.com.pe", "production-supervisor", facility: "Callao plant");

        var result = await _service.Handle(SignUp(email: "luis.rojas@andinos.com.pe", role: "administrator",
            environment: "production", organizationId: 2, privilege: "administrator"));

        Assert.True(result.IsSuccess);
        Assert.Equal(Roles.RoleProductionSupervisor, result.Value!.Role);
        Assert.Equal(1, result.Value.OrganizationId);
        Assert.Equal("Callao plant", result.Value.Facility);
        Assert.Equal("none", result.Value.Privilege);
        Assert.Equal(EInvitationStatus.Accepted, invitation.Status);
    }

    [Fact]
    public async Task AWrongPasswordOrAnUnknownEmailGiveTheSameReason()
    {
        await _service.Handle(SignUp());

        var wrongPassword = await _service.Handle(new SignInCommand("elena.paz@andinos.com.pe", "wrong", null));
        var unknownEmail = await _service.Handle(new SignInCommand("nobody@andinos.com.pe", "Secret123", null));

        Assert.Equal(IamError.InvalidCredentials, wrongPassword.Error);
        Assert.Equal(IamError.InvalidCredentials, unknownEmail.Error);
        Assert.Equal(UserCommandService.InvalidCredentials, unknownEmail.Message);
    }

    [Fact]
    public async Task AUserWithTwoFactorNeedsTheRightCode()
    {
        await _service.Handle(SignUp(twoFactorCode: "123456"));

        var withoutCode = await _service.Handle(new SignInCommand("elena.paz@andinos.com.pe", "Secret123", null));
        var wrongCode = await _service.Handle(new SignInCommand("elena.paz@andinos.com.pe", "Secret123", "000000"));
        var rightCode = await _service.Handle(new SignInCommand("elena.paz@andinos.com.pe", "Secret123", "123456"));

        Assert.Equal(IamError.TwoFactorRequired, withoutCode.Error);
        Assert.Equal(UserCommandService.TwoFactorRequired, withoutCode.Message);
        Assert.Equal(IamError.InvalidTwoFactorCode, wrongCode.Error);
        Assert.True(rightCode.IsSuccess);
        Assert.Equal("token-for:elena.paz@andinos.com.pe", rightCode.Value.token);
    }

    [Fact]
    public void RolesAreReadFromTheirKeyOrTheirName()
    {
        Assert.True(RolesExtensions.TryFromKey("ROLE_PRODUCTION_SUPERVISOR", out var byName));
        Assert.Equal(Roles.RoleProductionSupervisor, byName);
        Assert.True(RolesExtensions.TryFromKey("internal-auditor", out var byKey));
        Assert.Equal("ROLE_INTERNAL_AUDITOR", byKey.ToName());
        Assert.False(RolesExtensions.TryFromKey("chef", out _));
    }

    private sealed class InMemoryUserRepository : IUserRepository
    {
        public List<User> Items { get; } = [];

        public Task AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            Items.Add(entity);
            return Task.CompletedTask;
        }

        public Task<User?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.FirstOrDefault(user => user.Id == id));

        public Task<User?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            FindByIdAsync(id, cancellationToken);

        public void Update(User entity)
        {
        }

        public void Remove(User entity) => Items.Remove(entity);

        public Task<IEnumerable<User>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<User>>(Items);

        public Task<IEnumerable<User>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
            ListAsync(cancellationToken);

        public Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.FirstOrDefault(user => user.Email == email.Trim().ToLowerInvariant()));

        public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.Any(user => user.Email == email.Trim().ToLowerInvariant()));

        public Task<bool> ExistsByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.Any(user => user.OrganizationId == organizationId));
    }

    private sealed class InMemoryInvitationRepository : IInvitationRepository
    {
        public List<Invitation> Items { get; } = [];

        public Task AddAsync(Invitation entity, CancellationToken cancellationToken = default)
        {
            Items.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Invitation?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.FirstOrDefault(invitation => invitation.Id == id));

        public Task<Invitation?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            FindByIdAsync(id, cancellationToken);

        public void Update(Invitation entity)
        {
        }

        public void Remove(Invitation entity) => Items.Remove(entity);

        public Task<IEnumerable<Invitation>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Invitation>>(Items);

        public Task<IEnumerable<Invitation>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
            ListAsync(cancellationToken);

        public Task<bool> ExistsPendingByEmailAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.Any(invitation =>
                invitation.Email == email.Trim().ToLowerInvariant() && invitation.Status == EInvitationStatus.Pending));

        public Task<Invitation?> FindPendingByEmailAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.FirstOrDefault(invitation =>
                invitation.Email == email.Trim().ToLowerInvariant() && invitation.Status == EInvitationStatus.Pending));

        public Task<IEnumerable<Invitation>> FindAllByOrganizationIdAsync(int organizationId,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Invitation>>(Items.Where(invitation => invitation.OrganizationId == organizationId));
    }

    /// <summary>The hash of a value is "hash:" + value.</summary>
    private sealed class FakeHashingService : IHashingService
    {
        public string HashPassword(string password) => "hash:" + password;
        public bool VerifyPassword(string password, string passwordHash) => "hash:" + password == passwordHash;
    }

    private sealed class FakeTokenService : ITokenService
    {
        public string GenerateToken(User user) => "token-for:" + user.Email;
        public Task<int?> ValidateToken(string token) => Task.FromResult<int?>(null);
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public Task CompleteAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
