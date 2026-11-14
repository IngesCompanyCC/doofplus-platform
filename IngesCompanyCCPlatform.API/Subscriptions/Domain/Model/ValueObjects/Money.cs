using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Amount of money in a currency.
/// </summary>
public partial record Money
{
    private const string AmountInvalidMessageKey = "money.error.amount.invalid";
    private const string CurrencyPatternMessageKey = "money.error.currency.pattern";

    /// <param name="amount">
    ///     The non-negative amount.
    /// </param>
    /// <param name="currency">
    ///     The ISO 4217 currency code, for example <c>PEN</c>.
    /// </param>
    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException(AmountInvalidMessageKey);
        if (currency is null || !CurrencyPattern().IsMatch(currency)) throw new ArgumentException(CurrencyPatternMessageKey);
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public string Currency { get; }

    [GeneratedRegex("^[A-Z]{3}$")]
    private static partial Regex CurrencyPattern();
}
