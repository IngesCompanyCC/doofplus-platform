namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;

/// <summary>
///     Reads the enums sent by the clients. It accepts the PascalCase name (<c>NotFit</c>) and the kebab-case value
///     the Web App uses (<c>not-fit</c>), ignoring case.
/// </summary>
public static class EnumParser
{
    /// <summary>
    ///     Converts the text to the enum, or returns the default value when the text is empty.
    /// </summary>
    /// <param name="value">The text sent by the client.</param>
    /// <param name="defaultValue">The value used when the text is empty.</param>
    /// <param name="messageKey">The message key thrown when the text is not a value of the enum.</param>
    /// <typeparam name="TEnum">The enum.</typeparam>
    /// <returns>The value of the enum.</returns>
    /// <exception cref="ArgumentException">Thrown when the text is not a value of the enum.</exception>
    public static TEnum ParseOrDefault<TEnum>(string? value, TEnum defaultValue, string messageKey)
        where TEnum : struct, Enum
    {
        return string.IsNullOrWhiteSpace(value) ? defaultValue : Parse<TEnum>(value, messageKey);
    }

    /// <summary>
    ///     Converts the text to the enum.
    /// </summary>
    /// <param name="value">The text sent by the client.</param>
    /// <param name="messageKey">The message key thrown when the text is not a value of the enum.</param>
    /// <typeparam name="TEnum">The enum.</typeparam>
    /// <returns>The value of the enum.</returns>
    /// <exception cref="ArgumentException">Thrown when the text is empty or not a value of the enum.</exception>
    public static TEnum Parse<TEnum>(string? value, string messageKey) where TEnum : struct, Enum
    {
        var name = value?.Replace("-", string.Empty).Replace("_", string.Empty).Replace(" ", string.Empty);
        if (string.IsNullOrEmpty(name) || char.IsDigit(name[0])
            || !Enum.TryParse<TEnum>(name, true, out var result) || !Enum.IsDefined(result))
            throw new ArgumentException(messageKey);
        return result;
    }
}
