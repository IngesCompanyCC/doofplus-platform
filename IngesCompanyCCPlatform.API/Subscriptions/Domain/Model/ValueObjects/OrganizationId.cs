namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
/// Identifier of the organization that owns the subscription (Organizations &amp; Profiles bounded context).
/// </summary>
public record OrganizationId
{
    private const string InvalidMessageKey = "subscription.error.organizationId.invalid";

    public int Value { get; }

    public OrganizationId(int value)
    {
        if (value <= 0) throw new ArgumentException(InvalidMessageKey);
        Value = value;
    }
}
