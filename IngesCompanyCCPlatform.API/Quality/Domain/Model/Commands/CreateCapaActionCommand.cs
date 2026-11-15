using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a capa action.
/// </summary>
public record CreateCapaActionCommand(
    string CapaCode,
    string Code,
    ECapaType? Type,
    string Title,
    string Owner,
    DateOnly? DueAt,
    EActionStatus Status,
    string Evidence,
    bool EvidenceAttached);
