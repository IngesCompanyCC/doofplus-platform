namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the approval status of a quality document.
/// </summary>
public enum EDocumentStatus
{
    Draft,
    InReview,
    Approved,
    Obsolete
}
