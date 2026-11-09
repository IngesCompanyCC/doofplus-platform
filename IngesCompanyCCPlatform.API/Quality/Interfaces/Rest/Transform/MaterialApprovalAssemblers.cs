using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="MaterialApproval" /> aggregate into a <see cref="MaterialApprovalResource" />.
/// </summary>
public static class MaterialApprovalResourceFromEntityAssembler
{
    public static MaterialApprovalResource ToResourceFromEntity(MaterialApproval entity)
    {
        return new MaterialApprovalResource(entity.Id, entity.RawMaterialLotId, entity.SampleCode,
            entity.Status.ToString(), entity.DecidedBy?.Value, entity.SignatureId?.Value, entity.Reason);
    }
}

/// <summary>
///     Converts the request resources of the material approvals into commands.
/// </summary>
public static class MaterialApprovalCommandFromResourceAssembler
{
    public static RequestMaterialApprovalCommand ToCommandFromResource(RequestMaterialApprovalResource resource)
    {
        return new RequestMaterialApprovalCommand(new RawMaterialLotId(resource.RawMaterialLotId));
    }

    public static RegisterMaterialSampleCommand ToCommandFromResource(int materialApprovalId,
        RegisterMaterialSampleResource resource)
    {
        return new RegisterMaterialSampleCommand(materialApprovalId, resource.SampleCode);
    }

    public static ApproveMaterialCommand ToCommandFromResource(int materialApprovalId, ApproveMaterialResource resource)
    {
        return new ApproveMaterialCommand(materialApprovalId, new UserId(resource.DecidedBy),
            new ElectronicSignatureId(resource.SignatureId));
    }

    public static RejectMaterialCommand ToCommandFromResource(int materialApprovalId, RejectMaterialResource resource)
    {
        return new RejectMaterialCommand(materialApprovalId, new UserId(resource.DecidedBy), resource.Reason);
    }
}
