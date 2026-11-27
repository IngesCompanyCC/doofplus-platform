using System.Net;
using System.Net.Http.Json;
using IngesCompanyCCPlatform.API.Tests.Shared.Infrastructure;

namespace IngesCompanyCCPlatform.API.Tests.Manufacturing.Interfaces;

/// <summary>
///     Integration tests of <c>/api/v1/production-batches</c> and <c>/api/v1/production-orders</c> through HTTP, with the
///     real pipeline: token, controllers, command services and persistence (TS03, US14, US57).
/// </summary>
public class ProductionBatchesEndpointTests : IClassFixture<PlatformApiFactory>
{
    private readonly PlatformApiFactory _factory;

    public ProductionBatchesEndpointTests(PlatformApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task WithoutATokenTheBatchesAnswerUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/v1/production-batches");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ABatchIsRegisteredAndThenFoundByItsNumber()
    {
        var client = await _factory.CreateAuthenticatedClientAsync("batch.owner@andinos.com.pe");

        var created = await client.PostAsJsonAsync("/api/v1/production-batches", new
        {
            batchNumber = "B-90001",
            productionOrderId = 1,
            plannedQuantityValue = 1000m,
            plannedQuantityUnit = "tablets",
            line = "Line 2",
            owner = "Rosa Diaz"
        });
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);

        var found = await client.GetFromJsonAsync<List<BatchResponse>>("/api/v1/production-batches?batchNumber=B-90001");
        var batch = Assert.Single(found!);
        Assert.Equal("B-90001", batch.BatchNumber);
        Assert.Equal("Planned", batch.Status);
    }

    [Fact]
    public async Task ARepeatedBatchNumberAnswersConflict()
    {
        var client = await _factory.CreateAuthenticatedClientAsync("batch.duplicate@andinos.com.pe");
        var body = new
        {
            batchNumber = "B-90002",
            productionOrderId = 1,
            plannedQuantityValue = 500m,
            plannedQuantityUnit = "tablets"
        };

        await client.PostAsJsonAsync("/api/v1/production-batches", body);
        var repeated = await client.PostAsJsonAsync("/api/v1/production-batches", body);

        Assert.Equal(HttpStatusCode.Conflict, repeated.StatusCode);
    }

    [Fact]
    public async Task RejectingAnUnknownOrderAnswersNotFound()
    {
        var client = await _factory.CreateAuthenticatedClientAsync("order.reviewer@andinos.com.pe");

        var response = await client.PostAsJsonAsync("/api/v1/production-orders/9999/rejections",
            new { reason = "Wrong master formula." });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ChangingTheStatusOfAnUnknownOrderAnswersNotFound()
    {
        var client = await _factory.CreateAuthenticatedClientAsync("order.planner@andinos.com.pe");

        var response = await client.PutAsJsonAsync("/api/v1/production-orders/9999/status", new { status = "Approved" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ApprovingAnUnknownRawMaterialLotAnswersNotFound()
    {
        var client = await _factory.CreateAuthenticatedClientAsync("lot.inspector@andinos.com.pe");

        var response = await client.PostAsync("/api/v1/material-lots/9999/approvals", null);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private sealed record BatchResponse(string BatchNumber, string Status);
}
