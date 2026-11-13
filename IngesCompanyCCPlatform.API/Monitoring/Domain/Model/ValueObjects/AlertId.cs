using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

/// <summary>
///     The strongly typed id of an alert.
/// </summary>
/// <param name="Value">The integer value of the id.</param>
public readonly record struct AlertId(int Value) : IEntityId<int>
{
    /// <summary>
    ///     Converts the id to its integer value.
    /// </summary>
    public static implicit operator int(AlertId id) => id.Value;

    /// <summary>
    ///     Converts an integer to the id.
    /// </summary>
    public static implicit operator AlertId(int value) => new(value);

    /// <inheritdoc />
    public override string ToString() => Value.ToString();
}
