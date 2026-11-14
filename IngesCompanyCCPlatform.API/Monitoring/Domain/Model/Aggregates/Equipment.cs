using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;

/// <summary>
///     A piece of equipment of the plant, with its calibration and maintenance.
/// </summary>
public partial class Equipment : AggregateRoot
{
    private const string NameNotBlankMessageKey = "monitoring.equipment.error.name.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Equipment()
    {
        Code = null!;
        Name = string.Empty;
        Model = string.Empty;
        Location = string.Empty;
        Certificate = string.Empty;
        MaintenanceOwner = string.Empty;
        WorkOrder = string.Empty;
        HealthNote = string.Empty;
    }

    /// <summary>
    ///     Registers a piece of equipment.
    /// </summary>
    /// <param name="command">The data of the equipment.</param>
    /// <exception cref="ArgumentException">Thrown when the name is blank.</exception>
    public Equipment(CreateEquipmentCommand command) : this()
    {
        Code = command.Code;
        Apply(command.Name, command.Model, command.Location, command.CalibrationStatus, command.CalibrationDue,
            command.Certificate, command.NextMaintenance, command.MaintenanceOwner, command.WorkOrder,
            command.HealthNote);
    }

    /// <summary>
    ///     The id of the equipment.
    /// </summary>
    public EquipmentId Id { get; }

    /// <summary>
    ///     The business code, for example EQ-MIX-02.
    /// </summary>
    public EquipmentCode Code { get; private set; }

    /// <summary>
    ///     The name, for example High-shear mixer.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     The model and serial number.
    /// </summary>
    public string Model { get; private set; }

    /// <summary>
    ///     Where the equipment is, for example Ate · Line 02.
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    ///     Whether the equipment can be used according to its calibration.
    /// </summary>
    public ECalibrationStatus CalibrationStatus { get; private set; }

    /// <summary>
    ///     When the current calibration expires.
    /// </summary>
    public DateOnly? CalibrationDue { get; private set; }

    /// <summary>
    ///     The code of the calibration certificate.
    /// </summary>
    public string Certificate { get; private set; }

    /// <summary>
    ///     The date of the next maintenance.
    /// </summary>
    public DateOnly? NextMaintenance { get; private set; }

    /// <summary>
    ///     Who is in charge of the maintenance.
    /// </summary>
    public string MaintenanceOwner { get; private set; }

    /// <summary>
    ///     The open work order, if any.
    /// </summary>
    public string WorkOrder { get; private set; }

    /// <summary>
    ///     A short note on the state of the equipment.
    /// </summary>
    public string HealthNote { get; private set; }

    /// <summary>
    ///     Updates the data of the equipment; the code does not change.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when the name is blank.</exception>
    public void Update(UpdateEquipmentCommand command)
    {
        Apply(command.Name, command.Model, command.Location, command.CalibrationStatus, command.CalibrationDue,
            command.Certificate, command.NextMaintenance, command.MaintenanceOwner, command.WorkOrder,
            command.HealthNote);
    }

    private void Apply(string name, string model, string location, ECalibrationStatus calibrationStatus,
        DateOnly? calibrationDue, string certificate, DateOnly? nextMaintenance, string maintenanceOwner,
        string workOrder, string healthNote)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(NameNotBlankMessageKey);
        Name = name.Trim();
        Model = model?.Trim() ?? string.Empty;
        Location = location?.Trim() ?? string.Empty;
        CalibrationStatus = calibrationStatus;
        CalibrationDue = calibrationDue;
        Certificate = certificate?.Trim() ?? string.Empty;
        NextMaintenance = nextMaintenance;
        MaintenanceOwner = maintenanceOwner?.Trim() ?? string.Empty;
        WorkOrder = workOrder?.Trim() ?? string.Empty;
        HealthNote = healthNote?.Trim() ?? string.Empty;
    }
}
