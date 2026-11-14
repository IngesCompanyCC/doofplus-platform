using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

/// <summary>
///     The strongly typed id of a telemetry reading.
/// </summary>
/// <param name="Value">The integer value of the id.</param>
public readonly record struct ReadingId(int Value) : IEntityId<int>
{
    /// <summary>
    ///     Converts the id to its integer value.
    /// </summary>
    public static implicit operator int(ReadingId id) => id.Value;

    /// <summary>
    ///     Converts an integer to the id.
    /// </summary>
    public static implicit operator ReadingId(int value) => new(value);

    /// <inheritdoc />
    public override string ToString() => Value.ToString();
}
