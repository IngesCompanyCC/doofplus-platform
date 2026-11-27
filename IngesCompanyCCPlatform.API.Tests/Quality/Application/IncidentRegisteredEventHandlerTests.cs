using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Acl;
using IngesCompanyCCPlatform.API.Quality.Application.Internal.EventHandlers;
using IngesCompanyCCPlatform.API.Quality.Application.Internal.OutboundServices.Acl;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using Microsoft.Extensions.Logging.Abstractions;

namespace IngesCompanyCCPlatform.API.Tests.Quality.Application;

/// <summary>
///     Tests that an incident of a production batch opens a deviation in Quality.
/// </summary>
public class IncidentRegisteredEventHandlerTests
{
    private static readonly DateTime RecordedAt = new(2026, 11, 27, 15, 30, 12, DateTimeKind.Utc);

    private readonly FakeDeviationRepository _deviations = new();
    private readonly FakeManufacturingContextFacade _manufacturing = new();

    private IncidentRegisteredEventHandler Handler => new(
        _deviations, new ExternalManufacturingService(_manufacturing), NullLogger<IncidentRegisteredEventHandler>.Instance);

    private static IncidentRegisteredEvent Incident(string severity = "Major", string? line = "Line 2") =>
        new(7, "B-26041", line, "Granulator stopped for 20 minutes", severity, RecordedAt);

    [Fact]
    public void RegisteringAnIncidentShouldPublishTheEventWithTheBatchLineAndDescription()
    {
        var batch = new ProductionBatch(new CreateBatchCommand("B-26041", 7, 1000m, "tablets", "Line 2"));

        batch.RegisterIncident(new RegisterIncidentCommand(0, "Granulator stopped", IncidentSeverity.Critical));

        var published = Assert.IsType<IncidentRegisteredEvent>(Assert.Single(batch.DomainEvents));
        Assert.Equal("B-26041", published.BatchNumber);
        Assert.Equal("Line 2", published.Line);
        Assert.Equal("Granulator stopped", published.Description);
        Assert.Equal("Critical", published.Severity);
    }

    [Fact]
    public async Task AnIncidentShouldOpenADeviationLinkedToTheBatchAndItsOrder()
    {
        _manufacturing.OrderCode = "PO-26041";

        await Handler.Handle(Incident(), CancellationToken.None);

        var deviation = Assert.Single(_deviations.Added);
        Assert.Equal(EDeviationStatus.Open, deviation.Status);
        Assert.Equal("B-26041", deviation.BatchCode);
        Assert.Equal("PO-26041", deviation.OrderCode);
        Assert.Equal(EClassification.Major, deviation.Classification);
        Assert.Equal("DEV-INC-261127153012", deviation.Code);
        Assert.Contains("Granulator stopped for 20 minutes", deviation.Summary);
        Assert.Contains("Line 2", deviation.Summary);
        Assert.Equal(IncidentRegisteredEventHandler.Reporter, deviation.ReportedBy);
        Assert.Equal(RecordedAt, deviation.ReportedAt);
    }

    [Fact]
    public async Task ACriticalIncidentShouldOpenACriticalDeviationWithItsContainment()
    {
        await Handler.Handle(Incident("Critical"), CancellationToken.None);

        var deviation = Assert.Single(_deviations.Added);
        Assert.Equal(EClassification.Critical, deviation.Classification);
        Assert.Equal(5, deviation.Severity);
        Assert.NotEmpty(deviation.Containment);
    }

    [Fact]
    public async Task ABatchUnknownToManufacturingShouldStillOpenTheDeviationWithoutOrder()
    {
        await Handler.Handle(Incident(line: null), CancellationToken.None);

        var deviation = Assert.Single(_deviations.Added);
        Assert.Equal("B-26041", deviation.BatchCode);
        Assert.Equal(string.Empty, deviation.OrderCode);
        Assert.Equal("Granulator stopped for 20 minutes", deviation.Summary);
    }

    [Fact]
    public async Task ATakenCodeShouldGetASuffix()
    {
        _deviations.TakenCodes.Add("DEV-INC-261127153012");

        await Handler.Handle(Incident(), CancellationToken.None);

        Assert.Equal("DEV-INC-261127153012-2", Assert.Single(_deviations.Added).Code);
    }

    private sealed class FakeManufacturingContextFacade : IManufacturingContextFacade
    {
        public string OrderCode { get; set; } = string.Empty;

        public Task<bool> ExistsBatchByNumber(string batchNumber, CancellationToken cancellationToken = default) =>
            Task.FromResult(OrderCode != string.Empty);

        public Task<string> FetchBatchStatusByNumber(string batchNumber, CancellationToken cancellationToken = default) =>
            Task.FromResult(string.Empty);

        public Task<string> FetchProductionOrderCodeByBatchNumber(string batchNumber, CancellationToken cancellationToken = default) =>
            Task.FromResult(OrderCode);
    }

    private sealed class FakeDeviationRepository : IDeviationRepository
    {
        public List<Deviation> Added { get; } = [];
        public HashSet<string> TakenCodes { get; } = [];

        public Task AddAsync(Deviation entity, CancellationToken cancellationToken = default)
        {
            Added.Add(entity);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default) =>
            Task.FromResult(TakenCodes.Contains(code));

        public Task<IEnumerable<Deviation>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Deviation>>(Added.Where(d => d.BatchCode == batchCode).ToList());

        public Task<Deviation?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult<Deviation?>(null);

        public Task<Deviation?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult<Deviation?>(null);

        public void Update(Deviation entity)
        {
        }

        public void Remove(Deviation entity) => Added.Remove(entity);

        public Task<IEnumerable<Deviation>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Deviation>>(Added);

        public Task<IEnumerable<Deviation>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Deviation>>(Added);
    }
}
