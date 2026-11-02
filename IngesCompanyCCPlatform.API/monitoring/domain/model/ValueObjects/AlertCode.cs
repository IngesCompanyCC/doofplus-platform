using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public partial record AlertCode{
    private const int MaxLength = 80;
    private const string NotBlankMessageKey = "monitoring.alert.error.code.notBlank";
    private const string SizeMessageKey = "monitoring.alert.error.code.size";
    private const string PatternMessageKey = "monitoring.alert.error.code.pattern";

    public string Value { get; }

    public AlertCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("AlertCode cannot be null or empty.", nameof(value)); }

        if (value.Length > MaxLength) { throw new ArgumentException("AlertCode is too long.", nameof(value)); }

        if (!AllowedPattern().IsMatch(value)) { throw new ArgumentException("AlertCode has an invalid format.", nameof(value)); }

        Value = value;
    }

    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]

    private static partial Regex AllowedPattern();

}