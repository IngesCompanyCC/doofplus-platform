using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to record the incoming inspection of a lot.
/// </summary>
public record InspectRawMaterialLotCommand(
    RawMaterialLotId RawMaterialLotId,
    bool PackagingVerified,
    bool IdentityVerified,
    ESamplingStatus SamplingStatus,
    string StorageLocation);
