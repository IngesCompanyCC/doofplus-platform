using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to get a master formula by its id.
/// </summary>
public record GetMasterFormulaByIdQuery(
    MasterFormulaId MasterFormulaId);
