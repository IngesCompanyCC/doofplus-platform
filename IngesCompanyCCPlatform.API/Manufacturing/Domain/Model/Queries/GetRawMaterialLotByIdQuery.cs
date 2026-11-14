using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to get a lot of raw material by its id.
/// </summary>
public record GetRawMaterialLotByIdQuery(
    RawMaterialLotId RawMaterialLotId);
