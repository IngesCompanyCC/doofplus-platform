namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Data to receive a lot of raw material. <c>category</c> is Api, Excipient or Packaging.
/// </summary>
public record ReceiveRawMaterialLotResource(
    string Code,
    string? ReceiptCode,
    string Material,
    string Category,
    string? Supplier,
    string? SupplierLot,
    string? Quantity,
    DateOnly ExpiryDate,
    string? StorageLocation,
    string? Certificate);
