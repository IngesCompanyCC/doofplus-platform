using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     The strongly typed id of a master formula.
/// </summary>
/// <param name="Value">The integer value of the id.</param>
public readonly record struct MasterFormulaId(int Value) : IEntityId<int>
{
    /// <summary>
    ///     Converts the id to its integer value.
    /// </summary>
    public static implicit operator int(MasterFormulaId id) => id.Value;

    /// <summary>
    ///     Converts an integer to the id.
    /// </summary>
    public static implicit operator MasterFormulaId(int value) => new(value);

    /// <inheritdoc />
    public override string ToString() => Value.ToString();
}
