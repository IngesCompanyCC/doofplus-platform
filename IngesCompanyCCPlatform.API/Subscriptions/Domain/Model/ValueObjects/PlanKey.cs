using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
/// Business key of a subscription plan, for example <c>standard-lab</c> or <c>enterprise</c>.
/// </summary>
public partial record PlanKey
{
    private const int MaxLength = 64;
    private const string NotBlankMessageKey = "plan.error.planKey.notBlank";
    private const string SizeMessageKey = "plan.error.planKey.size";
    private const string PatternMessageKey = "plan.error.planKey.pattern";

    public string Value { get; }

    public PlanKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(NotBlankMessageKey);
        if (value.Length > MaxLength) throw new ArgumentException(SizeMessageKey);
        if (!AllowedPattern().IsMatch(value)) throw new ArgumentException(PatternMessageKey);
        Value = value;
    }

    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]
    private static partial Regex AllowedPattern();
}
