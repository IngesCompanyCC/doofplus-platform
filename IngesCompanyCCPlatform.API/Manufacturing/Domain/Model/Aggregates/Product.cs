using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     A pharmaceutical product of the laboratory, for example Paracetamol 500 mg.
/// </summary>
public partial class Product : AggregateRoot
{
    private const string NameNotBlankMessageKey = "manufacturing.product.error.name.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Product()
    {
        Code = null!;
        Name = string.Empty;
    }

    /// <summary>
    ///     Registers a product.
    /// </summary>
    /// <param name="command">The data of the product.</param>
    /// <exception cref="ArgumentException">Thrown when the name is blank.</exception>
    public Product(CreateProductCommand command) : this()
    {
        Code = command.Code;
        Apply(command.Name, command.DosageForm);
    }

    /// <summary>The id of the product.</summary>
    public ProductId Id { get; }

    /// <summary>The business code, for example AC500.</summary>
    public ProductCode Code { get; private set; }

    /// <summary>The name, for example Paracetamol 500 mg.</summary>
    public string Name { get; private set; }

    /// <summary>The pharmaceutical form.</summary>
    public EDosageForm DosageForm { get; private set; }

    /// <summary>
    ///     Updates the product; its code does not change.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when the name is blank.</exception>
    public void Update(UpdateProductCommand command)
    {
        Apply(command.Name, command.DosageForm);
    }

    private void Apply(string name, EDosageForm dosageForm)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(NameNotBlankMessageKey);
        Name = name.Trim();
        DosageForm = dosageForm;
    }
}
