using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a plan in the Subscriptions bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct PlanId(int Value) : IEntityId<int>
{
    public static implicit operator int(PlanId id) => id.Value;
    public static implicit operator PlanId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
