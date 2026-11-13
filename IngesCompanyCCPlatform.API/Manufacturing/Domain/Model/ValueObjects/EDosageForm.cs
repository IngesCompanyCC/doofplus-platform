namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     The pharmaceutical form of a product.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EDosageForm
{
    /// <summary>Tablet.</summary>
    Tablet,
    /// <summary>Capsule.</summary>
    Capsule,
    /// <summary>Syrup.</summary>
    Syrup,
    /// <summary>Injectable.</summary>
    Injectable,
    /// <summary>Cream.</summary>
    Cream
}
