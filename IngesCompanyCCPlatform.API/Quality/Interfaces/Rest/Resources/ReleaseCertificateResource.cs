namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing the certificate issued with the verdict on a batch.
/// </summary>
public record ReleaseCertificateResource(int Id, string CertificateNumber, string Verdict, int SignatureId, DateTime IssuedAt);
