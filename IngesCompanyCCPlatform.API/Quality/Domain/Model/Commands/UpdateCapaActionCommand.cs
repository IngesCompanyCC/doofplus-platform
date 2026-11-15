using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to replace the data of a capa action.
/// </summary>
public record UpdateCapaActionCommand(
    int CapaActionId,
    string CapaCode,
    string Code,
    ECapaType? Type,
    string Title,
    string Owner,
    DateOnly? DueAt,
    EActionStatus Status,
    string Evidence,
    bool EvidenceAttached);
