using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Events;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;

/// <summary>
///     Raised when Quality releases or rejects a production batch. Manufacturing updates the batch with it.
/// </summary>
public record BatchReleaseDecidedEvent(ProductionBatchId ProductionBatchId, EReleaseVerdict Verdict, string CertificateNumber) : IEvent;
