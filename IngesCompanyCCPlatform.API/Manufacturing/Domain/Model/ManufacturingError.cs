namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;

/// <summary>
///     Enumeration of domain errors for the Manufacturing bounded context.
/// </summary>
public enum ManufacturingError
{
    None,
    BatchNotFound,
    DuplicateBatchNumber,
    InvalidBatchState,
    InvalidOperation,
    DatabaseError,
    InternalServerError,

    /// <summary>The product does not exist.</summary>
    ProductNotFound,

    /// <summary>Another product already has the code.</summary>
    DuplicateProductCode,

    /// <summary>The master formula does not exist.</summary>
    MasterFormulaNotFound,

    /// <summary>The product already has a formula with the version.</summary>
    DuplicateFormulaVersion,

    /// <summary>The formula cannot change from its current status.</summary>
    InvalidFormulaState,

    /// <summary>The formula is not approved, so no order can follow it.</summary>
    FormulaNotApproved,

    /// <summary>The production order does not exist.</summary>
    ProductionOrderNotFound,

    /// <summary>Another production order already has the code.</summary>
    DuplicateProductionOrderCode,

    /// <summary>The production order cannot change from its current status.</summary>
    InvalidProductionOrderState,

    /// <summary>The production order has no operation with the sequence.</summary>
    OperationNotFound,

    /// <summary>The lot of raw material does not exist.</summary>
    RawMaterialLotNotFound,

    /// <summary>Another lot already has the code.</summary>
    DuplicateRawMaterialLotCode,

    /// <summary>The lot cannot change from its current status.</summary>
    InvalidRawMaterialLotState
}
