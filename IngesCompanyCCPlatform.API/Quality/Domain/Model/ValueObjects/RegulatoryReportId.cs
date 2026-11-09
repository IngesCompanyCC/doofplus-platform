using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a regulatory report in the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct RegulatoryReportId(int Value) : IEntityId<int>
{
    public static implicit operator int(RegulatoryReportId id) => id.Value;
    public static implicit operator RegulatoryReportId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
