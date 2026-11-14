using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     The business code of a product (for example AC500), for example the code shown in the Web App.
/// </summary>
/// <remarks>
///     Letters, digits and hyphens between them, up to 30 characters. Each broken rule throws an
///     <see cref="ArgumentException" /> whose message is the key of the localized error.
/// </remarks>
public partial record ProductCode
{
    private const int MaxLength = 30;
    private const string NotBlankMessageKey = "manufacturing.product.error.code.notBlank";
    private const string SizeMessageKey = "manufacturing.product.error.code.size";
    private const string PatternMessageKey = "manufacturing.product.error.code.pattern";

    /// <summary>
    ///     Creates the code after checking that it is not blank, its length and its characters.
    /// </summary>
    /// <param name="value">The code.</param>
    /// <exception cref="ArgumentException">Thrown when the code breaks one of the rules.</exception>
    public ProductCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(NotBlankMessageKey);
        if (value.Length > MaxLength) throw new ArgumentException(SizeMessageKey);
        if (!AllowedPattern().IsMatch(value)) throw new ArgumentException(PatternMessageKey);
        Value = value;
    }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;

    [GeneratedRegex("^[A-Za-z0-9]+(-[A-Za-z0-9]+)*$")]
    private static partial Regex AllowedPattern();
}
