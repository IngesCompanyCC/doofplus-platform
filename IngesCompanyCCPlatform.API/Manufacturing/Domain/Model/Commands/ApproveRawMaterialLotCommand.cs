using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to release a lot that passed its inspection.
/// </summary>
public record ApproveRawMaterialLotCommand(
    RawMaterialLotId RawMaterialLotId);
