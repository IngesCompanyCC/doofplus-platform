namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Result of the incoming inspection of a lot. <c>samplingStatus</c> is Requested or Done.
/// </summary>
public record InspectRawMaterialLotResource(
    bool PackagingVerified,
    bool IdentityVerified,
    string SamplingStatus,
    string? StorageLocation);
