
using IngesCompanyCCPlatform.API.Organizations.Application.Acl;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Tests.Organizations.Application;

/// <summary>
///     Tests of <see cref="OrganizationsContextFacade" />, with in-memory query services.
/// </summary>
public class OrganizationsContextFacadeTests
{
    private const int OrganizationId = 10;

    private readonly FakeOrganizationQueryService _organizations = new();

    private OrganizationsContextFacade Facade => new(_organizations);

    private static Organization Organization() =>
        new(new RegisterOrganizationCommand(
            "Andinos Pharmaceutical S.A.C.",
            "20123456789",
            "Lima",
            "Rosa Diaz"));

    [Fact]
    public async Task AnExistingOrganizationShouldBeFoundWithItsLegalName()
    {
        _organizations.Stored = Organization();

        Assert.True(await Facade.ExistsOrganizationById(OrganizationId));
        Assert.Equal(
            "Andinos Pharmaceutical S.A.C.",
            await Facade.FetchLegalNameById(OrganizationId));
        Assert.Equal(OrganizationId, _organizations.RequestedId);
    }

    [Fact]
    public async Task AMissingOrganizationShouldAnswerNotFoundWithAnEmptyLegalName()
    {
        Assert.False(await Facade.ExistsOrganizationById(OrganizationId));
        Assert.Equal(string.Empty, await Facade.FetchLegalNameById(OrganizationId));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task AnInvalidOrganizationIdShouldAnswerNotFoundWithoutQuerying(int organizationId)
    {
        _organizations.Stored = Organization();

        Assert.False(await Facade.ExistsOrganizationById(organizationId));
        Assert.Equal(string.Empty, await Facade.FetchLegalNameById(organizationId));
        Assert.Equal(0, _organizations.Calls);
    }

    private sealed class FakeOrganizationQueryService : IOrganizationQueryService
    {
        public Organization? Stored { get; set; }
        public int Calls { get; private set; }
        public int? RequestedId { get; private set; }

        public Task<Organization?> Handle(
            GetOrganizationByIdQuery query,
            CancellationToken cancellationToken = default)
        {
            Calls++;
            RequestedId = query.OrganizationId;
            return Task.FromResult(Stored);
        }

        public Task<IEnumerable<Organization>> Handle(
            GetAllOrganizationsQuery query,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Organization>>(
                Stored is null ? [] : [Stored]);

        public Task<Organization?> Handle(
            GetOrganizationByRucQuery query,
            CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored);
    }
}
