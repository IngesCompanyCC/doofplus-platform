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
    private readonly UserCommandService _service;

    public UserCommandServiceTests()
    {
        _service = new UserCommandService(_users, new FakeTokenService(), new FakeHashingService(), new FakeUnitOfWork());
    }

    private static SignUpCommand SignUp(string email = "elena.paz@andinos.com.pe", string password = "Secret123",
        string? role = "qa-specialist", string environment = "qa", string? twoFactorCode = null) =>
        new(email, password, "Elena Paz", role, environment, "Lima", null, null, 1, "Laboratorios Andinos",
            "Ate plant", twoFactorCode);

    [Fact]
    public async Task SignUpHashesThePasswordAndTheCode()
    {
        var result = await _service.Handle(SignUp(email: " Elena.Paz@andinos.com.pe ", twoFactorCode: "123456"));

        Assert.True(result.IsSuccess);
        var user = Assert.Single(_users.Items);
        Assert.Equal("elena.paz@andinos.com.pe", user.Email);
        Assert.Equal("hash:Secret123", user.PasswordHash);
        Assert.Equal("hash:123456", user.TwoFactorCodeHash);
        Assert.Equal(Roles.RoleQaSpecialist, user.Role);
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
