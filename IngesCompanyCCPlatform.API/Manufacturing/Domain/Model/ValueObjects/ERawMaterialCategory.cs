namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     The kind of raw material.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ERawMaterialCategory
{
    /// <summary>Active pharmaceutical ingredient.</summary>
    Api,
    /// <summary>Excipient.</summary>
    Excipient,
    /// <summary>Packaging material.</summary>
    Packaging
}
