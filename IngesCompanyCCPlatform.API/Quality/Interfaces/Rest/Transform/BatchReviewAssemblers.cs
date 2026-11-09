using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="BatchReview" /> aggregate into a <see cref="BatchReviewResource" />.
/// </summary>
public static class BatchReviewResourceFromEntityAssembler
{
    public static BatchReviewResource ToResourceFromEntity(BatchReview entity)
    {
        var certificate = entity.Certificate is null
            ? null
            : new ReleaseCertificateResource(entity.Certificate.Id, entity.Certificate.CertificateNumber,
                entity.Certificate.Verdict.ToString(), entity.Certificate.SignatureId, entity.Certificate.IssuedAt);
        return new BatchReviewResource(
            entity.Id,
            entity.ProductionBatchId,
            entity.Status.ToString(),
            entity.QuarantineStartedAt,
            entity.SampleCode,
            entity.EvaluatedAt,
            entity.ReleaseBlocked,
            entity.RejectionReason,
            entity.Evidences.Select(evidence =>
                new EvidenceAttachmentResource(evidence.Id, evidence.FileUrl, evidence.Description)),
            certificate);
    }
}

/// <summary>
///     Converts the request resources of the batch reviews into commands.
/// </summary>
public static class BatchReviewCommandFromResourceAssembler
{
    public static StartQuarantineCommand ToCommandFromResource(StartQuarantineResource resource)
    {
        return new StartQuarantineCommand(new ProductionBatchId(resource.ProductionBatchId));
    }

    public static RegisterBatchSampleCommand ToCommandFromResource(int batchReviewId,
        RegisterBatchSampleResource resource)
    {
        return new RegisterBatchSampleCommand(batchReviewId, resource.SampleCode);
    }

    public static AttachBatchEvidenceCommand ToCommandFromResource(int batchReviewId,
        AttachBatchEvidenceResource resource)
    {
        return new AttachBatchEvidenceCommand(batchReviewId, resource.FileUrl, resource.Description);
    }

    public static ReleaseBatchCommand ToCommandFromResource(int batchReviewId, ReleaseBatchResource resource)
    {
        return new ReleaseBatchCommand(batchReviewId, new ElectronicSignatureId(resource.SignatureId));
    }

    public static RejectBatchCommand ToCommandFromResource(int batchReviewId, RejectBatchResource resource)
    {
        return new RejectBatchCommand(batchReviewId, resource.Reason, new ElectronicSignatureId(resource.SignatureId));
    }
}
