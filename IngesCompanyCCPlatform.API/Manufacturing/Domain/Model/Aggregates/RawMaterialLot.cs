using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     A lot of raw material received from a supplier: an active ingredient, an excipient or packaging.
/// </summary>
/// <remarks>
///     It starts in <see cref="ERawMaterialLotStatus.Inspection" />. It can be approved only when its packaging and
///     identity are verified and its samples are taken, and only an approved lot can be allocated to a batch.
/// </remarks>
public partial class RawMaterialLot : AggregateRoot
{
    private const string MaterialNotBlankMessageKey = "manufacturing.lot.error.material.notBlank";
    private const string NotInInspectionMessageKey = "manufacturing.lot.error.notInInspection";
    private const string InspectionIncompleteMessageKey = "manufacturing.lot.error.inspectionIncomplete";
    private const string NotApprovedMessageKey = "manufacturing.lot.error.notApproved";
    private const string BatchNotBlankMessageKey = "manufacturing.lot.error.batch.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected RawMaterialLot()
    {
        Code = null!;
        ReceiptCode = string.Empty;
        Material = string.Empty;
        Supplier = string.Empty;
        SupplierLot = string.Empty;
        Quantity = string.Empty;
        StorageLocation = string.Empty;
        Certificate = string.Empty;
        AllocatedBatch = string.Empty;
    }

    /// <summary>
    ///     Receives a lot, in Inspection and with its samples requested.
    /// </summary>
    /// <param name="command">The data of the receipt.</param>
    /// <exception cref="ArgumentException">Thrown when the material is blank.</exception>
    public RawMaterialLot(ReceiveRawMaterialLotCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Material)) throw new ArgumentException(MaterialNotBlankMessageKey);
        Code = command.Code;
        ReceiptCode = command.ReceiptCode?.Trim() ?? string.Empty;
        Material = command.Material.Trim();
        Category = command.Category;
        Supplier = command.Supplier?.Trim() ?? string.Empty;
        SupplierLot = command.SupplierLot?.Trim() ?? string.Empty;
        Quantity = command.Quantity?.Trim() ?? string.Empty;
        ExpiryDate = command.ExpiryDate;
        StorageLocation = command.StorageLocation?.Trim() ?? string.Empty;
        Certificate = command.Certificate?.Trim() ?? string.Empty;
        Status = ERawMaterialLotStatus.Inspection;
        SamplingStatus = ESamplingStatus.Requested;
    }

    /// <summary>The id of the lot.</summary>
    public RawMaterialLotId Id { get; }

    /// <summary>The business code, for example RM-26098.</summary>
    public RawMaterialLotCode Code { get; private set; }

    /// <summary>The code of the receipt, for example RCV-26098.</summary>
    public string ReceiptCode { get; private set; }

    /// <summary>The material, for example Paracetamol (API).</summary>
    public string Material { get; private set; }

    /// <summary>The kind of material.</summary>
    public ERawMaterialCategory Category { get; private set; }

    /// <summary>The supplier.</summary>
    public string Supplier { get; private set; }

    /// <summary>The lot number of the supplier.</summary>
    public string SupplierLot { get; private set; }

    /// <summary>The received quantity and its packaging, for example 500 kg · 20 sealed drums.</summary>
    public string Quantity { get; private set; }

    /// <summary>When the material expires.</summary>
    public DateOnly ExpiryDate { get; private set; }

    /// <summary>Where it is stored, for example Q-A12 · Quarantine zone.</summary>
    public string StorageLocation { get; private set; }

    /// <summary>The certificate of analysis of the supplier.</summary>
    public string Certificate { get; private set; }

    /// <summary>The quality status.</summary>
    public ERawMaterialLotStatus Status { get; private set; }

    /// <summary>Whether the packaging was checked.</summary>
    public bool PackagingVerified { get; private set; }

    /// <summary>Whether the identity of the material was checked.</summary>
    public bool IdentityVerified { get; private set; }

    /// <summary>Whether Quality took the samples.</summary>
    public ESamplingStatus SamplingStatus { get; private set; }

    /// <summary>The code of the production batch it is allocated to, or empty.</summary>
    public string AllocatedBatch { get; private set; }

    /// <summary>
    ///     Records the incoming inspection of the lot.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the lot is no longer in Inspection.</exception>
    public void Inspect(InspectRawMaterialLotCommand command)
    {
        if (Status != ERawMaterialLotStatus.Inspection) throw new InvalidOperationException(NotInInspectionMessageKey);
        PackagingVerified = command.PackagingVerified;
        IdentityVerified = command.IdentityVerified;
        SamplingStatus = command.SamplingStatus;
        if (!string.IsNullOrWhiteSpace(command.StorageLocation)) StorageLocation = command.StorageLocation.Trim();
    }

    /// <summary>
    ///     Releases the lot for production.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the lot is not in Inspection or its inspection is incomplete.</exception>
    public void Approve()
    {
        if (Status != ERawMaterialLotStatus.Inspection) throw new InvalidOperationException(NotInInspectionMessageKey);
        if (!PackagingVerified || !IdentityVerified || SamplingStatus != ESamplingStatus.Done)
            throw new InvalidOperationException(InspectionIncompleteMessageKey);
        Status = ERawMaterialLotStatus.Approved;
    }

    /// <summary>
    ///     Rejects the lot.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the lot is not in Inspection.</exception>
    public void Reject()
    {
        if (Status != ERawMaterialLotStatus.Inspection) throw new InvalidOperationException(NotInInspectionMessageKey);
        Status = ERawMaterialLotStatus.Rejected;
    }

    /// <summary>
    ///     Allocates the lot to a production batch.
    /// </summary>
    /// <param name="batchCode">The code of the batch.</param>
    /// <exception cref="ArgumentException">Thrown when the batch code is blank.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the lot is not approved.</exception>
    public void AllocateTo(string batchCode)
    {
        if (string.IsNullOrWhiteSpace(batchCode)) throw new ArgumentException(BatchNotBlankMessageKey);
        if (Status != ERawMaterialLotStatus.Approved) throw new InvalidOperationException(NotApprovedMessageKey);
        AllocatedBatch = batchCode.Trim();
    }
}
