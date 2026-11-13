using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;

/// <summary>
///     A line of a master formula: a component with its target quantity, or a process parameter with its range.
/// </summary>
public class FormulaItem
{
    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected FormulaItem()
    {
        Name = string.Empty;
        Role = string.Empty;
        Target = string.Empty;
        Tolerance = string.Empty;
        Control = string.Empty;
    }

    /// <summary>
    ///     Creates a line of the formula.
    /// </summary>
    public FormulaItem(EFormulaItemKind kind, string name, string role, string target, string tolerance,
        string control) : this()
    {
        Kind = kind;
        Name = name.Trim();
        Role = role?.Trim() ?? string.Empty;
        Target = target?.Trim() ?? string.Empty;
        Tolerance = tolerance?.Trim() ?? string.Empty;
        Control = control?.Trim() ?? string.Empty;
    }

    /// <summary>The id of the line.</summary>
    public int Id { get; private set; }

    /// <summary>The id of its master formula.</summary>
    public MasterFormulaId MasterFormulaId { get; private set; }

    /// <summary>Whether it is a component or a parameter.</summary>
    public EFormulaItemKind Kind { get; private set; }

    /// <summary>The material or the parameter, for example Paracetamol (API).</summary>
    public string Name { get; private set; }

    /// <summary>The role of the component, for example Active ingredient.</summary>
    public string Role { get; private set; }

    /// <summary>The target, for example 60.0 kg or 21 °C.</summary>
    public string Target { get; private set; }

    /// <summary>The accepted tolerance, for example ± 0.5 %.</summary>
    public string Tolerance { get; private set; }

    /// <summary>How it is controlled, for example the supplier and lot, or a sensor.</summary>
    public string Control { get; private set; }
}
