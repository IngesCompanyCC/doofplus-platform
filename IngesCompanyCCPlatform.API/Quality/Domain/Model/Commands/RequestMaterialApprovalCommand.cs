using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to request the quality decision on a raw material lot (sent by Manufacturing).
/// </summary>
public record RequestMaterialApprovalCommand(RawMaterialLotId RawMaterialLotId);
