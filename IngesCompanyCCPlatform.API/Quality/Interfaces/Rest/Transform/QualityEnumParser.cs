namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts the text of a request into an enum of the Quality &amp; Compliance bounded context.
/// </summary>
internal static class QualityEnumParser
{
    /// <summary>
    ///     Parses a value ignoring the case, for example <c>critical</c> into <c>EDeviationSeverity.Critical</c>.
    /// </summary>
    /// <param name="value">The text sent in the request.</param>
    /// <param name="field">The name of the field, used in the error message.</param>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <returns>The enum value.</returns>
    /// <exception cref="ArgumentException">When the text is not a value of the enum.</exception>
    public static TEnum Parse<TEnum>(string value, string field) where TEnum : struct, Enum
    {
        if (Enum.TryParse<TEnum>(value, true, out var result) && Enum.IsDefined(result)) return result;
        throw new ArgumentException(
            $"The {field} '{value}' is not valid. Use one of: {string.Join(", ", Enum.GetNames<TEnum>())}.");
    }
}
