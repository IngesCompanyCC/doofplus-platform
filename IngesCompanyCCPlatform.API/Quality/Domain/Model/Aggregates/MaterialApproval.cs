using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the MaterialApproval aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Records the decision of Quality on a raw material lot received by Manufacturing: the lot is sampled
///     and then approved or rejected. Manufacturing learns the decision through <see cref="MaterialApprovalDecidedEvent" />.
/// </remarks>
public partial class MaterialApproval : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MaterialApproval" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected MaterialApproval()
    {
    }

    /// <summary>
    ///     Initializes a pending material approval for a raw material lot.
    /// </summary>
    /// <param name="command">The <see cref="RequestMaterialApprovalCommand" /> with the lot.</param>
    public MaterialApproval(RequestMaterialApprovalCommand command) : this()
    {
        RawMaterialLotId = command.RawMaterialLotId;
        Status = EMaterialApprovalStatus.Pending;
    }

    public MaterialApprovalId Id { get; }
    public RawMaterialLotId RawMaterialLotId { get; private set; }
    public string? SampleCode { get; private set; }
    public EMaterialApprovalStatus Status { get; private set; }
    public UserId? DecidedBy { get; private set; }

    /// <summary>
    ///     Gets the electronic signature of the approval, or <c>null</c> when the lot is not approved.
    /// </summary>
    public ElectronicSignatureId? SignatureId { get; private set; }

    /// <summary>
    ///     Gets the reason of the rejection, or <c>null</c> when the lot is not rejected.
    /// </summary>
    public string? Reason { get; private set; }

    /// <summary>
    ///     Registers the sample taken from the lot.
    /// </summary>
    /// <param name="sampleCode">The code of the sample.</param>
    public void RegisterSample(string sampleCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sampleCode);
        if (Status != EMaterialApprovalStatus.Pending)
            throw new InvalidOperationException("A sample can only be registered for a pending material approval.");
        SampleCode = sampleCode;
        Status = EMaterialApprovalStatus.Sampled;
    }

    /// <summary>
    ///     Approves a sampled lot.
    /// </summary>
    /// <param name="decidedBy">The user that approves the lot.</param>
    /// <param name="signatureId">The electronic signature registered in IAM.</param>
    public void Approve(UserId decidedBy, ElectronicSignatureId signatureId)
    {
        if (Status != EMaterialApprovalStatus.Sampled)
            throw new InvalidOperationException("Only a sampled raw material lot can be approved.");
        DecidedBy = decidedBy;
        SignatureId = signatureId;
        Status = EMaterialApprovalStatus.Approved;
        AddDomainEvent(new MaterialApprovalDecidedEvent(RawMaterialLotId, true));
    }

    /// <summary>
    ///     Rejects a lot that is still pending or sampled.
    /// </summary>
    /// <param name="decidedBy">The user that rejects the lot.</param>
    /// <param name="reason">Why the lot is rejected.</param>
    public void Reject(UserId decidedBy, string reason)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);
        if (Status is EMaterialApprovalStatus.Approved or EMaterialApprovalStatus.Rejected)
            throw new InvalidOperationException("The raw material lot already has a decision.");
        DecidedBy = decidedBy;
        Reason = reason;
        Status = EMaterialApprovalStatus.Rejected;
        AddDomainEvent(new MaterialApprovalDecidedEvent(RawMaterialLotId, false));
    }
}
