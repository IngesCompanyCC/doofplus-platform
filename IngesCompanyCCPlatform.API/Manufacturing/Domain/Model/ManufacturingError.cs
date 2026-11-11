namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;

/// <summary>
///     Enumeration of domain errors for the Manufacturing bounded context.
/// </summary>
public enum ManufacturingError
{
    None,
    BatchNotFound,
    DuplicateBatchNumber,
    BatchAlreadyClosed,
    BatchNotPlanned,
    InvalidOperation,
    DatabaseError,
    InternalServerError
}
