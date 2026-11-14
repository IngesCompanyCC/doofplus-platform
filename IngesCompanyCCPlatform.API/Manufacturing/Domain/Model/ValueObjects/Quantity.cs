namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a measured quantity with an amount and unit of measurement.
/// </summary>
public record Quantity
{
    public decimal Value { get; }
    public string Unit { get; }

    public Quantity(decimal value, string unit)
    {
        if (value <= 0)
            throw new ArgumentException("Quantity value must be greater than zero.", nameof(value));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Quantity unit cannot be empty.", nameof(unit));

        Value = value;
        Unit = unit.Trim();
    }

    public override string ToString() => $"{Value} {Unit}";
}
