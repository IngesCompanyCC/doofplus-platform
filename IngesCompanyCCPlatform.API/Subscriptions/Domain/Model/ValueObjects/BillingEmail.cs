using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
/// Email address that receives the invoices of the subscription.
/// </summary>
public partial record BillingEmail
{
    private const int MaxLength = 254;
    private const string NotBlankMessageKey = "subscription.error.billingEmail.notBlank";
    private const string SizeMessageKey = "subscription.error.billingEmail.size";
    private const string PatternMessageKey = "subscription.error.billingEmail.pattern";

    public string Value { get; }

    public BillingEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(NotBlankMessageKey);
        if (value.Length > MaxLength) throw new ArgumentException(SizeMessageKey);
        if (!AllowedPattern().IsMatch(value)) throw new ArgumentException(PatternMessageKey);
        Value = value;
    }

    [GeneratedRegex(@"^[\w.+-]+@[\w-]+(\.[\w-]+)+$")]
    private static partial Regex AllowedPattern();
}
