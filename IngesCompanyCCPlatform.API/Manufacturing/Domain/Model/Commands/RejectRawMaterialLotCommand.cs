using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to reject a lot in inspection.
/// </summary>
public record RejectRawMaterialLotCommand(
    RawMaterialLotId RawMaterialLotId);
