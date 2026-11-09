using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the material approval of a raw material lot.
/// </summary>
public record GetMaterialApprovalByRawMaterialLotIdQuery(RawMaterialLotId RawMaterialLotId);
