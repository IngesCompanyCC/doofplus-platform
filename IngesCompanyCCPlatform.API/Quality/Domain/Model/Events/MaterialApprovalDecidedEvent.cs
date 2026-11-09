using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Events;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;

/// <summary>
///     Raised when Quality approves or rejects a raw material lot. Manufacturing updates the lot with it.
/// </summary>
public record MaterialApprovalDecidedEvent(RawMaterialLotId RawMaterialLotId, bool Approved) : IEvent;
