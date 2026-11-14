namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     A lot of raw material, as the Web App shows it.
/// </summary>
public record RawMaterialLotResource(
    int Id,
    string Code,
    string ReceiptCode,
    string Material,
    string Category,
    string Supplier,
    string SupplierLot,
    string Quantity,
    DateOnly ExpiryDate,
    string StorageLocation,
    string Certificate,
    string Status,
    bool PackagingVerified,
    bool IdentityVerified,
    string SamplingStatus,
    string AllocatedBatch);
