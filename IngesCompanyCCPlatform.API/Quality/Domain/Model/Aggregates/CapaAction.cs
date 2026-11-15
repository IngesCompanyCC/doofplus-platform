using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Capa action of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class CapaAction : AggregateRoot
{
    private const string CapaCodeNotBlankMessageKey = "quality.capaAction.error.capaCode.notBlank";
    private const string CodeNotBlankMessageKey = "quality.capaAction.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.capaAction.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected CapaAction()
    {
        CapaCode = string.Empty;
        Code = string.Empty;
        Title = string.Empty;
        Owner = string.Empty;
        Evidence = string.Empty;
    }

    /// <summary>
    ///     Creates the capa action from its command.
    /// </summary>
    /// <param name="command">The data of the capa action.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public CapaAction(CreateCapaActionCommand command) : this() => Apply(command.CapaCode, command.Code, command.Type, command.Title, command.Owner, command.DueAt, command.Status, command.Evidence, command.EvidenceAttached);

    /// <summary>
    ///     The id of the capa action.
    /// </summary>
    public CapaActionId Id { get; }

    /// <summary>
    ///     The capa code.
    /// </summary>
    public string CapaCode { get; private set; }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     The type.
    /// </summary>
    public ECapaType Type { get; private set; }

    /// <summary>
    ///     The title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     The owner.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    ///     The due at.
    /// </summary>
    public DateOnly? DueAt { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EActionStatus Status { get; private set; }

    /// <summary>
    ///     The evidence.
    /// </summary>
    public string Evidence { get; private set; }

    /// <summary>
    ///     The evidence attached.
    /// </summary>
    public bool EvidenceAttached { get; private set; }

    /// <summary>
    ///     Replaces the data of the capa action with the data of the command.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public void Update(UpdateCapaActionCommand command) => Apply(command.CapaCode, command.Code, command.Type, command.Title, command.Owner, command.DueAt, command.Status, command.Evidence, command.EvidenceAttached);

    private void Apply(string capaCode, string code, ECapaType? type, string title, string owner, DateOnly? dueAt, EActionStatus status, string evidence, bool evidenceAttached)
    {
        if (string.IsNullOrWhiteSpace(capaCode)) throw new ArgumentException(CapaCodeNotBlankMessageKey);
        CapaCode = capaCode?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = code?.Trim() ?? string.Empty;
        Type = type ?? (Code.StartsWith("PA-", StringComparison.OrdinalIgnoreCase) ? ECapaType.Preventive : ECapaType.Corrective);
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = title?.Trim() ?? string.Empty;
        Owner = owner?.Trim() ?? string.Empty;
        DueAt = dueAt;
        Status = status;
        Evidence = evidence?.Trim() ?? string.Empty;
        EvidenceAttached = evidenceAttached;
    }
}
