using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     A version of the master formula of a product: its components and process parameters for a batch size.
/// </summary>
/// <remarks>
///     It starts as <see cref="EFormulaStatus.Draft" />. Only an approved formula can be followed by a production order.
/// </remarks>
public partial class MasterFormula : AggregateRoot
{
    private const string VersionNotBlankMessageKey = "manufacturing.formula.error.version.notBlank";
    private const string BatchSizeInvalidMessageKey = "manufacturing.formula.error.batchSize.invalid";
    private const string ItemsEmptyMessageKey = "manufacturing.formula.error.items.empty";
    private const string ItemNameNotBlankMessageKey = "manufacturing.formula.error.itemName.notBlank";
    private const string NotDraftMessageKey = "manufacturing.formula.error.notDraft";
    private const string ApproverNotBlankMessageKey = "manufacturing.formula.error.approvedBy.notBlank";

    private readonly List<FormulaItem> _items = [];

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected MasterFormula()
    {
        ProductCode = null!;
        Version = string.Empty;
        ApprovedBy = string.Empty;
    }

    /// <summary>
    ///     Writes a new version of the formula of a product, in Draft.
    /// </summary>
    /// <param name="command">The product, the version, the batch size and the lines of the formula.</param>
    /// <exception cref="ArgumentException">Thrown when the version is blank, the batch size is not positive or there are no lines.</exception>
    public MasterFormula(CreateMasterFormulaCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Version)) throw new ArgumentException(VersionNotBlankMessageKey);
        if (command.BatchSize <= 0) throw new ArgumentException(BatchSizeInvalidMessageKey);
        if (command.Items is null || command.Items.Count == 0) throw new ArgumentException(ItemsEmptyMessageKey);
        ProductCode = command.ProductCode;
        Version = command.Version.Trim();
        BatchSize = command.BatchSize;
        Status = EFormulaStatus.Draft;
        foreach (var item in command.Items)
        {
            if (string.IsNullOrWhiteSpace(item.Name)) throw new ArgumentException(ItemNameNotBlankMessageKey);
            _items.Add(new FormulaItem(item.Kind, item.Name, item.Role, item.Target, item.Tolerance, item.Control));
        }
    }

    /// <summary>The id of the formula.</summary>
    public MasterFormulaId Id { get; }

    /// <summary>The code of the product.</summary>
    public ProductCode ProductCode { get; private set; }

    /// <summary>The version, for example v3.2.</summary>
    public string Version { get; private set; }

    /// <summary>Where the formula is in its life cycle.</summary>
    public EFormulaStatus Status { get; private set; }

    /// <summary>Who approved it, or empty.</summary>
    public string ApprovedBy { get; private set; }

    /// <summary>When it was approved.</summary>
    public DateOnly? ApprovedAt { get; private set; }

    /// <summary>The batch size the quantities are written for, in units of the product.</summary>
    public decimal BatchSize { get; private set; }

    /// <summary>The components and process parameters.</summary>
    public IReadOnlyCollection<FormulaItem> Items => _items.AsReadOnly();

    /// <summary>Whether production orders can follow it.</summary>
    public bool IsApproved => Status == EFormulaStatus.Approved;

    /// <summary>
    ///     Approves the formula.
    /// </summary>
    /// <param name="approvedBy">Who approves it.</param>
    /// <exception cref="ArgumentException">Thrown when the approver is blank.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the formula is not in Draft or Pending.</exception>
    public void Approve(string approvedBy)
    {
        if (string.IsNullOrWhiteSpace(approvedBy)) throw new ArgumentException(ApproverNotBlankMessageKey);
        if (Status is not (EFormulaStatus.Draft or EFormulaStatus.Pending))
            throw new InvalidOperationException(NotDraftMessageKey);
        Status = EFormulaStatus.Approved;
        ApprovedBy = approvedBy.Trim();
        ApprovedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    /// <summary>
    ///     Marks an approved formula as replaced by a newer approved version.
    /// </summary>
    public void Lock()
    {
        if (Status == EFormulaStatus.Approved) Status = EFormulaStatus.Locked;
    }
}
