using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Manufacturing.Domain;

/// <summary>
///     Tests of the life cycle of a <see cref="ProductionBatch" /> (US14, US16, US56) and of the status changes of a
///     <see cref="ProductionOrder" /> (US57).
/// </summary>
public class ProductionLifecycleTests
{
    private static ProductionBatch Batch() =>
        new(new CreateBatchCommand("B-26041", 7, 1000m, "tablets", "Line 2", "Rosa Diaz"));

    private static ProductionOrder Order() =>
        new(new CreateProductionOrderCommand(
                new ProductionOrderCode("PO-26041"), new MasterFormulaId(1), "B-26041", 1000m, "Line 2", null, null, []),
            new ProductCode("PARA-500"));

    [Fact]
    public void ANewBatchIsPlannedWithItsData()
    {
        var batch = Batch();

        Assert.Equal(BatchStatus.Planned, batch.Status);
        Assert.Equal("B-26041", batch.BatchNumber.Value);
        Assert.Equal(1000m, batch.PlannedQuantity.Value);
        Assert.Equal(0, batch.Progress);
    }

    [Fact]
    public void ABatchGoesFromPlannedToReleased()
    {
        var batch = Batch();

        batch.Start();
        batch.Close(new CloseBatchCommand(0, 980m, "tablets"));
        Assert.Equal(BatchStatus.ReleaseRequested, batch.Status);
        Assert.Equal(100, batch.Progress);
        Assert.Equal(980m, batch.ManufacturedQuantity!.Value);

        batch.Release("Maria Mexico");
        Assert.Equal(BatchStatus.Released, batch.Status);
    }

    [Fact]
    public void ABatchRecordsItsEventsInOrder()
    {
        var batch = Batch();
        var before = batch.Events.Count;

        batch.Start();
        batch.Close(new CloseBatchCommand(0, 980m, "tablets"));

        Assert.Equal(before + 2, batch.Events.Count);
        Assert.Equal("RELEASE_REQUESTED", batch.Events.Last().Type);
    }

    [Fact]
    public void AStartedBatchCannotStartAgain()
    {
        var batch = Batch();
        batch.Start();

        Assert.Throws<InvalidOperationException>(() => batch.Start());
    }

    [Fact]
    public void APlannedBatchCannotBeClosed()
    {
        var batch = Batch();

        Assert.Throws<InvalidOperationException>(() => batch.Close(new CloseBatchCommand(0, 980m, "tablets")));
        Assert.Equal(BatchStatus.Planned, batch.Status);
    }

    [Fact]
    public void ABatchInProgressCannotBeReleasedBeforeItIsClosed()
    {
        var batch = Batch();
        batch.Start();

        Assert.Throws<InvalidOperationException>(() => batch.Release("Maria Mexico"));
        Assert.Equal(BatchStatus.InProgress, batch.Status);
    }

    [Fact]
    public void ACriticalIncidentPutsTheBatchOnHoldUntilItResumes()
    {
        var batch = Batch();
        batch.Start();

        batch.RegisterIncident(new RegisterIncidentCommand(0, "Coating temperature out of range", IncidentSeverity.Critical));
        Assert.Equal(BatchStatus.OnHold, batch.Status);
        Assert.Single(batch.Incidents);

        batch.Resume();
        Assert.Equal(BatchStatus.InProgress, batch.Status);
    }

    [Fact]
    public void AMinorIncidentDoesNotStopTheBatch()
    {
        var batch = Batch();
        batch.Start();

        batch.RegisterIncident(new RegisterIncidentCommand(0, "Label printer jam", IncidentSeverity.Minor));

        Assert.Equal(BatchStatus.InProgress, batch.Status);
    }

    [Fact]
    public void ABatchThatIsNotOnHoldCannotResume()
    {
        var batch = Batch();
        batch.Start();

        Assert.Throws<InvalidOperationException>(() => batch.Resume());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void AProgressOutsideZeroToOneHundredIsRejected(int progress)
    {
        var batch = Batch();

        Assert.Throws<ArgumentException>(() => batch.UpdateProgress(progress));
    }

    [Fact]
    public void AnOrderGoesFromPlannedToCompleted()
    {
        var order = Order();

        order.ChangeStatus(EProductionOrderStatus.Approved);
        order.ChangeStatus(EProductionOrderStatus.InProgress);
        order.ChangeStatus(EProductionOrderStatus.OnHold);
        order.ChangeStatus(EProductionOrderStatus.InProgress);
        order.ChangeStatus(EProductionOrderStatus.Completed);

        Assert.Equal(EProductionOrderStatus.Completed, order.Status);
    }

    [Theory]
    [InlineData(EProductionOrderStatus.Completed)]
    [InlineData(EProductionOrderStatus.OnHold)]
    public void APlannedOrderCannotSkipSteps(EProductionOrderStatus next)
    {
        var order = Order();

        Assert.Throws<InvalidOperationException>(() => order.ChangeStatus(next));
        Assert.Equal(EProductionOrderStatus.Planned, order.Status);
    }

    [Fact]
    public void AnOrderWithoutPositiveQuantityIsRejected()
    {
        Assert.Throws<ArgumentException>(() => new ProductionOrder(
            new CreateProductionOrderCommand(new ProductionOrderCode("PO-26042"), new MasterFormulaId(1), "B-26042", 0m,
                "Line 2", null, null, []),
            new ProductCode("PARA-500")));
    }

    [Fact]
    public void AnOrderWithRepeatedOperationSequencesIsRejected()
    {
        Assert.Throws<ArgumentException>(() => new ProductionOrder(
            new CreateProductionOrderCommand(new ProductionOrderCode("PO-26043"), new MasterFormulaId(1), "B-26043", 500m,
                "Line 2", null, null, [new OperationData(1, "Mixing", "EQ-MIX-01"), new OperationData(1, "Coating", "EQ-COAT-02")]),
            new ProductCode("PARA-500")));
    }
}
