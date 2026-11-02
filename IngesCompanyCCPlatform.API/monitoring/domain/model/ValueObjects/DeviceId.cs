using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public partial record DeviceId{
    private const int MaxLength = 128;
    private const string NotBlankMessageKey = "monitoring.device.error.id.notBlank";
    private const string SizeMessageKey = "monitoring.device.error.id.size";
    private const string PatternMessageKey = "monitoring.device.error.id.pattern";

    public string Value { get; }

    public DeviceId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException(NotBlankMessageKey); }

        if (value.Length > MaxLength) { throw new ArgumentException(SizeMessageKey); }

        if (!AllowedPattern().IsMatch(value)) { throw new ArgumentException(PatternMessageKey); }

        Value = value;
    }

    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]

    private static partial Regex AllowedPattern();

}