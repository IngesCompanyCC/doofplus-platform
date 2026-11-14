using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Entity Framework Core mapping of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     One table per aggregate, as in <c>db-06-quality</c>. The enums are stored as text and the codes are unique.
/// </remarks>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the mapping of the Quality &amp; Compliance bounded context.
    /// </summary>
    /// <param name="builder">The model builder of the database context.</param>
    public static void ApplyQualityConfiguration(this ModelBuilder builder)
    {
        // QualityDocuments
        builder.Entity<QualityDocument>().HasKey(e => e.Id);
        builder.Entity<QualityDocument>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<QualityDocumentId, int>(id => id.Value, value => new QualityDocumentId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<QualityDocument>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<QualityDocument>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<QualityDocument>().Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Entity<QualityDocument>().Property(e => e.DocumentType).IsRequired().HasMaxLength(60);
        builder.Entity<QualityDocument>().Property(e => e.FileReference).IsRequired().HasMaxLength(255);
        builder.Entity<QualityDocument>().Property(e => e.Version).IsRequired().HasMaxLength(20);
        builder.Entity<QualityDocument>().Property(e => e.OwnerId);
        builder.Entity<QualityDocument>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<QualityDocument>().Property(e => e.ApprovedAt);
        builder.Entity<QualityDocument>().HasIndex(e => e.Code).IsUnique();

        // Deviations
        builder.Entity<Deviation>().HasKey(e => e.Id);
        builder.Entity<Deviation>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<DeviationId, int>(id => id.Value, value => new DeviationId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Deviation>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<Deviation>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<Deviation>().Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<Deviation>().Property(e => e.Severity).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Deviation>().Property(e => e.BatchId).IsRequired();
        builder.Entity<Deviation>().Property(e => e.AssignedTo);
        builder.Entity<Deviation>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Deviation>().Property(e => e.DetectedAt).IsRequired();
        builder.Entity<Deviation>().Property(e => e.ClosedAt);
        builder.Entity<Deviation>().HasIndex(e => e.Code).IsUnique();
        builder.Entity<Deviation>().HasIndex(e => e.BatchId);

        // Evidence
        builder.Entity<Evidence>().HasKey(e => e.Id);
        builder.Entity<Evidence>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<EvidenceId, int>(id => id.Value, value => new EvidenceId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Evidence>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<Evidence>().Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Evidence>().Property(e => e.Type).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Evidence>().Property(e => e.FileReference).IsRequired().HasMaxLength(255);
        builder.Entity<Evidence>().Property(e => e.DeviationId);
        builder.Entity<Evidence>().Property(e => e.DocumentId);
        builder.Entity<Evidence>().Property(e => e.UploadedBy);
        builder.Entity<Evidence>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Evidence>().Property(e => e.UploadedAt).IsRequired();
        builder.Entity<Evidence>().Property(e => e.VerifiedAt);
        builder.Entity<Evidence>().HasIndex(e => e.DeviationId);

        // CapaPlans
        builder.Entity<CapaPlan>().HasKey(e => e.Id);
        builder.Entity<CapaPlan>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<CapaPlanId, int>(id => id.Value, value => new CapaPlanId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<CapaPlan>().Property(e => e.DeviationId).IsRequired();
        builder.Entity<CapaPlan>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<CapaPlan>().Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<CapaPlan>().Property(e => e.DueDate).IsRequired();
        builder.Entity<CapaPlan>().Property(e => e.Responsible).IsRequired().HasMaxLength(100);
        builder.Entity<CapaPlan>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<CapaPlan>().Property(e => e.CompletedAt);

        // CapaActions
        builder.Entity<CapaAction>().HasKey(e => e.Id);
        builder.Entity<CapaAction>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<CapaActionId, int>(id => id.Value, value => new CapaActionId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<CapaAction>().Property(e => e.CapaPlanId).IsRequired();
        builder.Entity<CapaAction>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<CapaAction>().Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<CapaAction>().Property(e => e.AssignedTo).IsRequired().HasMaxLength(100);
        builder.Entity<CapaAction>().Property(e => e.DueDate).IsRequired();
        builder.Entity<CapaAction>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<CapaAction>().Property(e => e.CompletedAt);
        builder.Entity<CapaAction>().HasIndex(e => e.CapaPlanId);

        // BatchReleases
        builder.Entity<BatchRelease>().HasKey(e => e.Id);
        builder.Entity<BatchRelease>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<BatchReleaseId, int>(id => id.Value, value => new BatchReleaseId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<BatchRelease>().Property(e => e.BatchId).IsRequired();
        builder.Entity<BatchRelease>().Property(e => e.BatchCode).IsRequired().HasMaxLength(30);
        builder.Entity<BatchRelease>().Property(e => e.RequestedBy).IsRequired().HasMaxLength(100);
        builder.Entity<BatchRelease>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<BatchRelease>().Property(e => e.Decision).IsRequired().HasMaxLength(500);
        builder.Entity<BatchRelease>().Property(e => e.SignatureReference).IsRequired().HasMaxLength(100);
        builder.Entity<BatchRelease>().Property(e => e.SignedBy).IsRequired().HasMaxLength(100);
        builder.Entity<BatchRelease>().Property(e => e.SignedAt);
        builder.Entity<BatchRelease>().HasIndex(e => e.BatchId);

        // AnalyticalResults
        builder.Entity<AnalyticalResult>().HasKey(e => e.Id);
        builder.Entity<AnalyticalResult>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AnalyticalResultId, int>(id => id.Value, value => new AnalyticalResultId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AnalyticalResult>().Property(e => e.BatchId).IsRequired();
        builder.Entity<AnalyticalResult>().Property(e => e.TestName).IsRequired().HasMaxLength(100);
        builder.Entity<AnalyticalResult>().Property(e => e.Parameter).IsRequired().HasMaxLength(100);
        builder.Entity<AnalyticalResult>().Property(e => e.MeasuredValue).HasPrecision(14, 4).IsRequired();
        builder.Entity<AnalyticalResult>().Property(e => e.MinimumValue).HasPrecision(14, 4).IsRequired();
        builder.Entity<AnalyticalResult>().Property(e => e.MaximumValue).HasPrecision(14, 4).IsRequired();
        builder.Entity<AnalyticalResult>().Property(e => e.Unit).IsRequired().HasMaxLength(20);
        builder.Entity<AnalyticalResult>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<AnalyticalResult>().Property(e => e.ReviewedBy);
        builder.Entity<AnalyticalResult>().Property(e => e.TestedAt);
        builder.Entity<AnalyticalResult>().Property(e => e.ReviewedAt);
        builder.Entity<AnalyticalResult>().HasIndex(e => e.BatchId);

        // Audits
        builder.Entity<Audit>().HasKey(e => e.Id);
        builder.Entity<Audit>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AuditId, int>(id => id.Value, value => new AuditId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Audit>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<Audit>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<Audit>().Property(e => e.Scope).IsRequired().HasMaxLength(255);
        builder.Entity<Audit>().Property(e => e.AuditType).IsRequired().HasMaxLength(60);
        builder.Entity<Audit>().Property(e => e.AuditorId);
        builder.Entity<Audit>().Property(e => e.PlannedDate);
        builder.Entity<Audit>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Audit>().Property(e => e.StartedAt);
        builder.Entity<Audit>().Property(e => e.CompletedAt);
        builder.Entity<Audit>().HasIndex(e => e.Code).IsUnique();

        // AuditFindings
        builder.Entity<AuditFinding>().HasKey(e => e.Id);
        builder.Entity<AuditFinding>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AuditFindingId, int>(id => id.Value, value => new AuditFindingId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AuditFinding>().Property(e => e.AuditId).IsRequired();
        builder.Entity<AuditFinding>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<AuditFinding>().Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<AuditFinding>().Property(e => e.Severity).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<AuditFinding>().Property(e => e.RelatedDeviationId);
        builder.Entity<AuditFinding>().Property(e => e.IdentifiedAt).IsRequired();
        builder.Entity<AuditFinding>().HasIndex(e => e.AuditId);

        // RegulatoryReports
        builder.Entity<RegulatoryReport>().HasKey(e => e.Id);
        builder.Entity<RegulatoryReport>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<RegulatoryReportId, int>(id => id.Value, value => new RegulatoryReportId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<RegulatoryReport>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<RegulatoryReport>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<RegulatoryReport>().Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<RegulatoryReport>().Property(e => e.RegulatoryAuthority).IsRequired().HasMaxLength(100);
        builder.Entity<RegulatoryReport>().Property(e => e.FileReference).IsRequired().HasMaxLength(255);
        builder.Entity<RegulatoryReport>().Property(e => e.DueDate);
        builder.Entity<RegulatoryReport>().Property(e => e.PreparedBy);
        builder.Entity<RegulatoryReport>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<RegulatoryReport>().Property(e => e.SubmittedAt);
        builder.Entity<RegulatoryReport>().HasIndex(e => e.Code).IsUnique();

        // CollaborationTasks
        builder.Entity<CollaborationTask>().HasKey(e => e.Id);
        builder.Entity<CollaborationTask>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<CollaborationTaskId, int>(id => id.Value, value => new CollaborationTaskId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<CollaborationTask>().Property(e => e.Title).IsRequired().HasMaxLength(150);
        builder.Entity<CollaborationTask>().Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<CollaborationTask>().Property(e => e.AssignedTo);
        builder.Entity<CollaborationTask>().Property(e => e.CreatedBy);
        builder.Entity<CollaborationTask>().Property(e => e.RelatedDeviationId);
        builder.Entity<CollaborationTask>().Property(e => e.DueDate);
        builder.Entity<CollaborationTask>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<CollaborationTask>().Property(e => e.CompletedAt);

        // TaskComments
        builder.Entity<TaskComment>().HasKey(e => e.Id);
        builder.Entity<TaskComment>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<TaskCommentId, int>(id => id.Value, value => new TaskCommentId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<TaskComment>().Property(e => e.TaskId).IsRequired();
        builder.Entity<TaskComment>().Property(e => e.AuthorId).IsRequired();
        builder.Entity<TaskComment>().Property(e => e.Content).IsRequired().HasMaxLength(1000);
        builder.Entity<TaskComment>().HasIndex(e => e.TaskId);

        // TraceabilityGaps
        builder.Entity<TraceabilityGap>().HasKey(e => e.Id);
        builder.Entity<TraceabilityGap>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<TraceabilityGapId, int>(id => id.Value, value => new TraceabilityGapId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<TraceabilityGap>().Property(e => e.BatchId).IsRequired();
        builder.Entity<TraceabilityGap>().Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<TraceabilityGap>().Property(e => e.MissingReference).IsRequired().HasMaxLength(100);
        builder.Entity<TraceabilityGap>().Property(e => e.AffectedProcess).IsRequired().HasMaxLength(100);
        builder.Entity<TraceabilityGap>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<TraceabilityGap>().Property(e => e.ReviewedBy);
        builder.Entity<TraceabilityGap>().Property(e => e.DetectedAt).IsRequired();
        builder.Entity<TraceabilityGap>().Property(e => e.ResolvedAt);
        builder.Entity<TraceabilityGap>().HasIndex(e => e.BatchId);

        // AuditEvents
        builder.Entity<AuditEvent>().HasKey(e => e.Id);
        builder.Entity<AuditEvent>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AuditEventId, int>(id => id.Value, value => new AuditEventId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AuditEvent>().Property(e => e.AggregateType).IsRequired().HasMaxLength(60);
        builder.Entity<AuditEvent>().Property(e => e.AggregateId).IsRequired();
        builder.Entity<AuditEvent>().Property(e => e.EventType).IsRequired().HasMaxLength(60);
        builder.Entity<AuditEvent>().Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Entity<AuditEvent>().Property(e => e.PerformedBy).IsRequired().HasMaxLength(100);
        builder.Entity<AuditEvent>().Property(e => e.OccurredAt).IsRequired();
        builder.Entity<AuditEvent>().HasIndex(e => e.AggregateType);
        builder.Entity<AuditEvent>().HasIndex(e => e.AggregateId);
    }
}
