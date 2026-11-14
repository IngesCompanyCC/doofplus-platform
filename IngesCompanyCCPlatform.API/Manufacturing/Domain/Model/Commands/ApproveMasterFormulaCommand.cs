using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to approve a master formula in Draft.
/// </summary>
public record ApproveMasterFormulaCommand(
    MasterFormulaId MasterFormulaId,
    string ApprovedBy);
