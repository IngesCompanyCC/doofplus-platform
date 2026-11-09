using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the material approval query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IMaterialApprovalQueryService
{
    Task<IEnumerable<MaterialApproval>> Handle(GetAllMaterialApprovalsQuery query, CancellationToken cancellationToken);

    Task<MaterialApproval?> Handle(GetMaterialApprovalByIdQuery query, CancellationToken cancellationToken);

    Task<MaterialApproval?> Handle(GetMaterialApprovalByRawMaterialLotIdQuery query,
        CancellationToken cancellationToken);
}
