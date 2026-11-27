using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Manufacturing.Domain;

/// <summary>
///     Tests of the value objects of Manufacturing: codes, batch number and quantity.
/// </summary>
public class ManufacturingValueObjectsTests
{
    private static readonly string TooLong = new('A', 31);

    [Theory]
    [InlineData("PARA-500")]
    [InlineData("AMOX500")]
    public void AValidProductCodeIsKept(string value)
    {
        Assert.Equal(value, new ProductCode(value).Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("PARA 500")]
    [InlineData("-PARA")]
    [InlineData("PARA--500")]
    public void AnInvalidProductCodeIsRejected(string value)
    {
        Assert.Throws<ArgumentException>(() => new ProductCode(value));
    }

    [Fact]
    public void ACodeLongerThanThirtyCharactersIsRejected()
    {
        Assert.Throws<ArgumentException>(() => new ProductCode(TooLong));
        Assert.Throws<ArgumentException>(() => new ProductionOrderCode(TooLong));
        Assert.Throws<ArgumentException>(() => new RawMaterialLotCode(TooLong));
    }

    [Fact]
    public void ValidOrderAndRawMaterialLotCodesAreKept()
    {
        Assert.Equal("PO-26041", new ProductionOrderCode("PO-26041").Value);
        Assert.Equal("RM-2611-07", new RawMaterialLotCode("RM-2611-07").Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("PO 26041")]
    public void AnInvalidOrderCodeIsRejected(string value)
    {
        Assert.Throws<ArgumentException>(() => new ProductionOrderCode(value));
    }

    [Fact]
    public void AValidBatchNumberIsKept()
    {
        Assert.Equal("B-26041", new BatchNumber("B-26041").Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ABlankBatchNumberIsRejected(string value)
    {
        Assert.Throws<ArgumentException>(() => new BatchNumber(value));
    }

    [Fact]
    public void AQuantityKeepsItsValueAndUnit()
    {
        var quantity = new Quantity(1000m, "tablets");

        Assert.Equal(1000m, quantity.Value);
        Assert.Equal("tablets", quantity.Unit);
        Assert.Equal("1000 tablets", quantity.ToString());
    }

    [Theory]
    [InlineData(0, "tablets")]
    [InlineData(-5, "tablets")]
    [InlineData(10, "")]
    public void AnInvalidQuantityIsRejected(decimal value, string unit)
    {
        Assert.Throws<ArgumentException>(() => new Quantity(value, unit));
    }
}
