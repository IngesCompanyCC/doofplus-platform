namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     What a line of a master formula describes.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EFormulaItemKind
{
    /// <summary>A material and its target quantity.</summary>
    Component,
    /// <summary>A process parameter and its accepted range.</summary>
    Parameter
}
