using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the material approval command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IMaterialApprovalCommandService
{
    /// <summary>Opens the material approval of a raw material lot.</summary>
    Task<Result<MaterialApproval>> Handle(RequestMaterialApprovalCommand command, CancellationToken cancellationToken);

    /// <summary>Registers the sample of a raw material lot.</summary>
    Task<Result<MaterialApproval>> Handle(RegisterMaterialSampleCommand command, CancellationToken cancellationToken);

    /// <summary>Approves a sampled raw material lot.</summary>
    Task<Result<MaterialApproval>> Handle(ApproveMaterialCommand command, CancellationToken cancellationToken);

    /// <summary>Rejects a raw material lot.</summary>
    Task<Result<MaterialApproval>> Handle(RejectMaterialCommand command, CancellationToken cancellationToken);
}
