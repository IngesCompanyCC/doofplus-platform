using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a subscription in the Subscriptions bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct SubscriptionId(int Value) : IEntityId<int>
{
    public static implicit operator int(SubscriptionId id) => id.Value;
    public static implicit operator SubscriptionId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
