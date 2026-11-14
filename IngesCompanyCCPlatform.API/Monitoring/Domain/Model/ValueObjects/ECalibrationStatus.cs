namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

/// <summary>
///     Whether a piece of equipment can be used according to its calibration.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ECalibrationStatus
{
    /// <summary>Calibrated and fit for use.</summary>
    Fit,
    /// <summary>The calibration is about to expire.</summary>
    Due,
    /// <summary>The calibration expired: not fit for use.</summary>
    NotFit
}
