using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Entity Framework Core mapping of the IoT Monitoring bounded context.
/// </summary>
/// <remarks>
///     The tables are <c>equipment</c>, <c>sensors</c>, <c>readings</c> and <c>alerts</c>, like the collections of
///     the Web App. The enums are stored as text and the codes are unique.
/// </remarks>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the mapping of the IoT Monitoring bounded context.
    /// </summary>
    /// <param name="builder">The model builder of the database context.</param>
    public static void ApplyMonitoringConfiguration(this ModelBuilder builder)
    {
        // Equipment
        builder.Entity<Equipment>().HasKey(e => e.Id);
        builder.Entity<Equipment>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<EquipmentId, int>(id => id.Value, value => new EquipmentId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Equipment>().Property(e => e.Code)
            .HasConversion(code => code.Value, value => new EquipmentCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<Equipment>().HasIndex(e => e.Code).IsUnique();
        builder.Entity<Equipment>().Property(e => e.Name).IsRequired().HasMaxLength(150);
        builder.Entity<Equipment>().Property(e => e.Model).IsRequired().HasMaxLength(150);
        builder.Entity<Equipment>().Property(e => e.Location).IsRequired().HasMaxLength(100);
        builder.Entity<Equipment>().Property(e => e.CalibrationStatus).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<Equipment>().Property(e => e.CalibrationDue);
        builder.Entity<Equipment>().Property(e => e.Certificate).IsRequired().HasMaxLength(60);
        builder.Entity<Equipment>().Property(e => e.NextMaintenance);
        builder.Entity<Equipment>().Property(e => e.MaintenanceOwner).IsRequired().HasMaxLength(100);
        builder.Entity<Equipment>().Property(e => e.WorkOrder).IsRequired().HasMaxLength(30);
        builder.Entity<Equipment>().Property(e => e.HealthNote).IsRequired().HasMaxLength(200);

        // Sensors
        builder.Entity<Sensor>().HasKey(s => s.Id);
        builder.Entity<Sensor>().Property(s => s.Id)
            .HasConversion(new StronglyTypedIdConverter<SensorId, int>(id => id.Value, value => new SensorId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Sensor>().Property(s => s.Code)
            .HasConversion(code => code.Value, value => new SensorCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<Sensor>().HasIndex(s => s.Code).IsUnique();
        builder.Entity<Sensor>().Property(s => s.DeviceId)
            .HasConversion(deviceId => deviceId.Value, value => new DeviceId(value))
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<Sensor>().HasIndex(s => s.DeviceId).IsUnique();
        builder.Entity<Sensor>().Property(s => s.Type).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<Sensor>().Property(s => s.Unit).IsRequired().HasMaxLength(10);
        builder.Entity<Sensor>().Property(s => s.EquipmentCode)
            .HasConversion(code => code.Value, value => new EquipmentCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<Sensor>().HasIndex(s => s.EquipmentCode);
        builder.Entity<Sensor>().Property(s => s.LowerLimit).HasPrecision(12, 4).IsRequired();
        builder.Entity<Sensor>().Property(s => s.UpperLimit).HasPrecision(12, 4).IsRequired();
        builder.Entity<Sensor>().Property(s => s.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<Sensor>().Property(s => s.AssignedBatch).IsRequired().HasMaxLength(30);
        builder.Entity<Sensor>().Ignore(s => s.IsAssignedToBatch);

        // Readings
        builder.Entity<Reading>().HasKey(r => r.Id);
        builder.Entity<Reading>().Property(r => r.Id)
            .HasConversion(new StronglyTypedIdConverter<ReadingId, int>(id => id.Value, value => new ReadingId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Reading>().Property(r => r.SensorCode)
            .HasConversion(code => code.Value, value => new SensorCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<Reading>().Property(r => r.BatchCode).IsRequired().HasMaxLength(30);
        builder.Entity<Reading>().Property(r => r.Value).HasPrecision(12, 4).IsRequired();
        builder.Entity<Reading>().Property(r => r.RecordedAt).IsRequired();
        builder.Entity<Reading>().Property(r => r.Source).IsRequired().HasMaxLength(30);
        builder.Entity<Reading>().HasIndex(r => new { r.SensorCode, r.RecordedAt });
        builder.Entity<Reading>().HasIndex(r => r.BatchCode);

        // Alerts
        builder.Entity<Alert>().HasKey(a => a.Id);
        builder.Entity<Alert>().Property(a => a.Id)
            .HasConversion(new StronglyTypedIdConverter<AlertId, int>(id => id.Value, value => new AlertId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Alert>().Property(a => a.Code)
            .HasConversion(code => code.Value, value => new AlertCode(value))
            .IsRequired()
            .HasMaxLength(50);
        builder.Entity<Alert>().HasIndex(a => a.Code).IsUnique();
        builder.Entity<Alert>().Property(a => a.SensorCode).IsRequired().HasMaxLength(30);
        builder.Entity<Alert>().Property(a => a.EquipmentCode).IsRequired().HasMaxLength(30);
        builder.Entity<Alert>().Property(a => a.Severity).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<Alert>().Property(a => a.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Alert>().Property(a => a.Title).IsRequired().HasMaxLength(150);
        builder.Entity<Alert>().Property(a => a.Detail).IsRequired().HasMaxLength(500);
        builder.Entity<Alert>().Property(a => a.BatchCode).IsRequired().HasMaxLength(30);
        builder.Entity<Alert>().Property(a => a.AcknowledgedBy).IsRequired().HasMaxLength(100);
        builder.Entity<Alert>().Property(a => a.AcknowledgedAt);
        builder.Entity<Alert>().Property(a => a.ResponseNote).IsRequired().HasMaxLength(500);
        builder.Entity<Alert>().Property(a => a.LinkedRecord).IsRequired().HasMaxLength(30);
        builder.Entity<Alert>().HasIndex(a => a.SensorCode);
        builder.Entity<Alert>().HasIndex(a => a.BatchCode);
    }
}
