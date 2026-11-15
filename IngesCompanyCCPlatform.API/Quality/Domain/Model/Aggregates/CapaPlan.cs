using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Capa plan of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class CapaPlan : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.capaPlan.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.capaPlan.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected CapaPlan()
    {
        Code = string.Empty;
        Title = string.Empty;
        SourceDeviation = string.Empty;
        Owner = string.Empty;
        Reviewer = string.Empty;
        RootCause = string.Empty;
        AcceptanceCriterion = string.Empty;
    }

    /// <summary>
    ///     Creates the capa plan from its command.
    /// </summary>
    /// <param name="command">The data of the capa plan.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public CapaPlan(CreateCapaPlanCommand command) : this() => Apply(command.Code, command.Title, command.SourceDeviation, command.Owner, command.Reviewer, command.RootCause, command.AcceptanceCriterion, command.EffectivenessCheckAt, command.Status);

    /// <summary>
    ///     The id of the capa plan.
    /// </summary>
    public CapaPlanId Id { get; }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     The title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     The source deviation.
    /// </summary>
    public string SourceDeviation { get; private set; }

    /// <summary>
    ///     The owner.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    ///     The reviewer.
    /// </summary>
    public string Reviewer { get; private set; }

    /// <summary>
    ///     The root cause.
    /// </summary>
    public string RootCause { get; private set; }

    /// <summary>
    ///     The acceptance criterion.
    /// </summary>
    public string AcceptanceCriterion { get; private set; }

    /// <summary>
    ///     The effectiveness check at.
    /// </summary>
    public DateOnly? EffectivenessCheckAt { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public ECapaStatus Status { get; private set; }

    /// <summary>
    ///     Replaces the data of the capa plan with the data of the command.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public void Update(UpdateCapaPlanCommand command) => Apply(command.Code, command.Title, command.SourceDeviation, command.Owner, command.Reviewer, command.RootCause, command.AcceptanceCriterion, command.EffectivenessCheckAt, command.Status);

    private void Apply(string code, string title, string sourceDeviation, string owner, string reviewer, string rootCause, string acceptanceCriterion, DateOnly? effectivenessCheckAt, ECapaStatus status)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = code?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = title?.Trim() ?? string.Empty;
        SourceDeviation = sourceDeviation?.Trim() ?? string.Empty;
        Owner = owner?.Trim() ?? string.Empty;
        Reviewer = reviewer?.Trim() ?? string.Empty;
        RootCause = rootCause?.Trim() ?? string.Empty;
        AcceptanceCriterion = acceptanceCriterion?.Trim() ?? string.Empty;
        EffectivenessCheckAt = effectivenessCheckAt;
        Status = status;
    }
}
