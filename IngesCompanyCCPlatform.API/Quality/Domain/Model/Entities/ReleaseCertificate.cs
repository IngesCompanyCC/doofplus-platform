using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;

/// <summary>
///     Represents the signed certificate with the verdict on a production batch.
/// </summary>
public class ReleaseCertificate
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ReleaseCertificate" /> entity. Used by Entity Framework Core.
    /// </summary>
    protected ReleaseCertificate()
    {
        CertificateNumber = string.Empty;
    }

    /// <param name="certificateNumber">The unique number of the certificate.</param>
    /// <param name="verdict">The verdict on the batch.</param>
    /// <param name="signatureId">The electronic signature of the Quality Manager.</param>
    public ReleaseCertificate(string certificateNumber, EReleaseVerdict verdict, ElectronicSignatureId signatureId)
    {
        CertificateNumber = certificateNumber;
        Verdict = verdict;
        SignatureId = signatureId;
        IssuedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public string CertificateNumber { get; private set; }
    public EReleaseVerdict Verdict { get; private set; }
    public ElectronicSignatureId SignatureId { get; private set; }
    public DateTime IssuedAt { get; private set; }
}
