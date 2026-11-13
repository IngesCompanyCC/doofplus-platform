using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to allocate an approved lot to a production batch.
/// </summary>
public record AllocateRawMaterialLotCommand(
    RawMaterialLotId RawMaterialLotId,
    string BatchCode);
