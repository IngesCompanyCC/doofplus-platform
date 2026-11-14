using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;

/// <summary>
///     Domain errors factory for the Manufacturing bounded context.
/// </summary>
public static class ManufacturingErrors
{
    public static readonly Error BatchNotFound =
        Error.NotFound("Manufacturing.BatchNotFound", "The specified production batch was not found.");

    public static readonly Error DuplicateBatchNumber =
        Error.Conflict("Manufacturing.DuplicateBatchNumber", "A production batch with the specified batch number already exists.");

    public static Error DuplicateBatchNumberWithCode(string batchNumber) =>
        Error.Conflict("Manufacturing.DuplicateBatchNumber", $"A production batch with number '{batchNumber}' already exists.");

    public static readonly Error InvalidBatchState =
        Error.Conflict("Manufacturing.InvalidBatchState", "The production batch cannot change to that status from its current one.");

    public static readonly Error InvalidOperation =
        Error.Failure("Manufacturing.InvalidOperation", "The requested manufacturing operation is invalid in the current batch state.");
}
