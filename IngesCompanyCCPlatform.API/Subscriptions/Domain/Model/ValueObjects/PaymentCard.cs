using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Card used to pay the subscription. Only the brand, the last four digits and the expiry are kept;
///     the full card number never reaches DoofPlus because Niubiz tokenizes it.
/// </summary>
public partial record PaymentCard
{
    private const string BrandNotBlankMessageKey = "subscription.error.cardBrand.notBlank";
    private const string BrandSizeMessageKey = "subscription.error.cardBrand.size";
    private const int BrandMaxLength = 20;
    private const string Last4PatternMessageKey = "subscription.error.cardLast4.pattern";
    private const string ExpiryPatternMessageKey = "subscription.error.cardExpiry.pattern";

    /// <param name="brand">
    ///     The card brand, for example <c>Visa</c>.
    /// </param>
    /// <param name="last4">
    ///     The last four digits of the card.
    /// </param>
    /// <param name="expiry">
    ///     The expiry date in <c>MM/yyyy</c> format.
    /// </param>
    public PaymentCard(string brand, string last4, string expiry)
    {
        if (string.IsNullOrWhiteSpace(brand)) throw new ArgumentException(BrandNotBlankMessageKey);
        if (brand.Length > BrandMaxLength) throw new ArgumentException(BrandSizeMessageKey);
        if (last4 is null || !Last4Pattern().IsMatch(last4)) throw new ArgumentException(Last4PatternMessageKey);
        if (expiry is null || !ExpiryPattern().IsMatch(expiry)) throw new ArgumentException(ExpiryPatternMessageKey);
        Brand = brand;
        Last4 = last4;
        Expiry = expiry;
    }

    public string Brand { get; }
    public string Last4 { get; }
    public string Expiry { get; }

    [GeneratedRegex(@"^\d{4}$")]
    private static partial Regex Last4Pattern();

    [GeneratedRegex(@"^(0[1-9]|1[0-2])/\d{4}$")]
    private static partial Regex ExpiryPattern();
}
