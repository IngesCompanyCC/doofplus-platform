using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to receive a lot of raw material; it starts in Inspection.
/// </summary>
public record ReceiveRawMaterialLotCommand(
    RawMaterialLotCode Code,
    string ReceiptCode,
    string Material,
    ERawMaterialCategory Category,
    string Supplier,
    string SupplierLot,
    string Quantity,
    DateOnly ExpiryDate,
    string StorageLocation,
    string Certificate);
