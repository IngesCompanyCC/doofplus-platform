namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing the quality decision on a raw material lot.
/// </summary>
public record MaterialApprovalResource(int Id, int RawMaterialLotId, string? SampleCode, string Status, int? DecidedBy, int? SignatureId, string? Reason);
