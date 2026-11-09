using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to register the sample taken from a raw material lot.
/// </summary>
public record RegisterMaterialSampleCommand(MaterialApprovalId MaterialApprovalId, string SampleCode);
