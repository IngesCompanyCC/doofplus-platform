using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Manufacturing.Domain;

/// <summary>
///     Tests of the rejection of a <see cref="ProductionOrder" /> with its reason (US57).
/// </summary>
public class ProductionOrderRejectionTests
{
    private static ProductionOrder Order() =>
        new(new CreateProductionOrderCommand(
                new ProductionOrderCode("PO-26041"), new MasterFormulaId(1), "B-26041", 1000m, "Line 2", null, null, []),
            new ProductCode("PARA-500"));

    [Fact]
    public void APlannedOrderIsRejectedWithItsReason()
    {
        var order = Order();

        order.Reject("  The planned quantity exceeds the line capacity.  ");

        Assert.Equal(EProductionOrderStatus.Rejected, order.Status);
        Assert.Equal("The planned quantity exceeds the line capacity.", order.RejectionReason);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ARejectionWithoutReasonIsNotAllowed(string reason)
    {
        var order = Order();

        Assert.Throws<ArgumentException>(() => order.Reject(reason));
        Assert.Equal(EProductionOrderStatus.Planned, order.Status);
        Assert.Equal(string.Empty, order.RejectionReason);
    }

    [Fact]
    public void AnOrderThatIsNoLongerPlannedCannotBeRejected()
    {
        var order = Order();
        order.ChangeStatus(EProductionOrderStatus.Approved);

        Assert.Throws<InvalidOperationException>(() => order.Reject("Late rejection."));
        Assert.Equal(EProductionOrderStatus.Approved, order.Status);
    }

    [Theory]
    [InlineData(EProductionOrderStatus.Approved)]
    [InlineData(EProductionOrderStatus.InProgress)]
    public void ARejectedOrderCannotBeApprovedOrStarted(EProductionOrderStatus next)
    {
        var order = Order();
        order.Reject("Wrong master formula.");

        Assert.Throws<InvalidOperationException>(() => order.ChangeStatus(next));
        Assert.Equal(EProductionOrderStatus.Rejected, order.Status);
    }

    [Fact]
    public void TheStatusEndpointCannotRejectAnOrder()
    {
        var order = Order();

        Assert.Throws<InvalidOperationException>(() => order.ChangeStatus(EProductionOrderStatus.Rejected));
        Assert.Equal(EProductionOrderStatus.Planned, order.Status);
    }
}
