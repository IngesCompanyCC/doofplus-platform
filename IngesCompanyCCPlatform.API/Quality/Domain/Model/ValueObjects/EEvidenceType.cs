namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     The kind of a piece of evidence.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EEvidenceType
{
    Document,
    Image,
    LabResult,
    Record,
    Other
}
