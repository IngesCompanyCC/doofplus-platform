using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a material approval by its id.
/// </summary>
public record GetMaterialApprovalByIdQuery(MaterialApprovalId MaterialApprovalId);
