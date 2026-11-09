using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the material approval command service of the Quality &amp; Compliance bounded context.
/// </summary>
public class MaterialApprovalCommandService(
    IMaterialApprovalRepository materialApprovalRepository,
    IUnitOfWork unitOfWork) : IMaterialApprovalCommandService
{
    /// <inheritdoc />
    public async Task<Result<MaterialApproval>> Handle(RequestMaterialApprovalCommand command,
        CancellationToken cancellationToken)
    {
        if (await materialApprovalRepository.FindByRawMaterialLotIdAsync(command.RawMaterialLotId,
                cancellationToken) is not null)
            return Result<MaterialApproval>.Failure(
                QualityErrors.MaterialApprovalAlreadyRequested(command.RawMaterialLotId),
                QualityError.MaterialApprovalAlreadyRequested);

        var materialApproval = new MaterialApproval(command);
        await materialApprovalRepository.AddAsync(materialApproval, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<MaterialApproval>.Success(materialApproval);
    }

    /// <inheritdoc />
    public Task<Result<MaterialApproval>> Handle(RegisterMaterialSampleCommand command,
        CancellationToken cancellationToken)
    {
        return Update(command.MaterialApprovalId, approval => approval.RegisterSample(command.SampleCode),
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<MaterialApproval>> Handle(ApproveMaterialCommand command, CancellationToken cancellationToken)
    {
        return Update(command.MaterialApprovalId,
            approval => approval.Approve(command.DecidedBy, command.SignatureId), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<MaterialApproval>> Handle(RejectMaterialCommand command, CancellationToken cancellationToken)
    {
        return Update(command.MaterialApprovalId,
            approval => approval.Reject(command.DecidedBy, command.Reason), cancellationToken);
    }

    private async Task<Result<MaterialApproval>> Update(MaterialApprovalId id, Action<MaterialApproval> behavior,
        CancellationToken cancellationToken)
    {
        var materialApproval = await materialApprovalRepository.FindByIdAsync(id, cancellationToken);
        if (materialApproval is null)
            return Result<MaterialApproval>.Failure(QualityErrors.MaterialApprovalNotFound(id),
                QualityError.MaterialApprovalNotFound);

        var result = QualityDomainRules.Apply(materialApproval, behavior);
        if (result.IsFailure) return result;

        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }
}
