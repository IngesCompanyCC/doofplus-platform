namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the accepted range of an analytical test, for example 95.0 to 105.0 %.
/// </summary>
public record SpecificationRange
{
    private const string UnitNotBlankMessageKey = "analyticalResult.error.specification.unit.notBlank";
    private const string RangeMessageKey = "analyticalResult.error.specification.range";

    /// <param name="min">The lowest accepted value.</param>
    /// <param name="max">The highest accepted value.</param>
    /// <param name="unit">The unit of measure, for example <c>%</c> or <c>mg</c>.</param>
    public SpecificationRange(decimal min, decimal max, string unit)
    {
        if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException(UnitNotBlankMessageKey);
        if (min > max) throw new ArgumentException(RangeMessageKey);
        Min = min;
        Max = max;
        Unit = unit;
    }

    public decimal Min { get; init; }
    public decimal Max { get; init; }
    public string Unit { get; init; }

    /// <summary>
    ///     Checks whether a value is inside the range, both limits included.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> when the value meets the specification.</returns>
    public bool Contains(decimal value) => value >= Min && value <= Max;
}
