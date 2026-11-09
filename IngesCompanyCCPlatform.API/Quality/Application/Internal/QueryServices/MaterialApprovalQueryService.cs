using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the material approval query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class MaterialApprovalQueryService(IMaterialApprovalRepository materialApprovalRepository)
    : IMaterialApprovalQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<MaterialApproval>> Handle(GetAllMaterialApprovalsQuery query,
        CancellationToken cancellationToken)
    {
        return await materialApprovalRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MaterialApproval?> Handle(GetMaterialApprovalByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await materialApprovalRepository.FindByIdAsNoTrackingAsync(query.MaterialApprovalId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MaterialApproval?> Handle(GetMaterialApprovalByRawMaterialLotIdQuery query,
        CancellationToken cancellationToken)
    {
        return await materialApprovalRepository.FindByRawMaterialLotIdAsync(query.RawMaterialLotId, cancellationToken);
    }
}
