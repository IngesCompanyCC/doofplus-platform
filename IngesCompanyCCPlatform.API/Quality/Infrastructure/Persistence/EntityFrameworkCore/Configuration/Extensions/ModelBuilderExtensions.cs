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
///     <c>procedure</c>, <c>before</c> and <c>after</c> are reserved words of MySQL, so those columns have another name.
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
        builder.Entity<QualityDocument>().Property(e => e.Version).IsRequired().HasMaxLength(10);
        builder.Entity<QualityDocument>().Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Entity<QualityDocument>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<QualityDocument>().Property(e => e.Author).IsRequired().HasMaxLength(100);
        builder.Entity<QualityDocument>().Property(e => e.TechnicalReviewer).IsRequired().HasMaxLength(100);
        builder.Entity<QualityDocument>().Property(e => e.Approver).IsRequired().HasMaxLength(100);
        builder.Entity<QualityDocument>().Property(e => e.ChangedAt);
        builder.Entity<QualityDocument>().Property(e => e.Purpose).IsRequired().HasColumnType("text");
        builder.Entity<QualityDocument>().Property(e => e.Scope).IsRequired().HasColumnType("text");
        builder.Entity<QualityDocument>().Property(e => e.Procedure).IsRequired().HasColumnType("text").HasColumnName("procedure_text");
        builder.Entity<QualityDocument>().Property(e => e.RevisionSummary).IsRequired().HasColumnType("text");
        builder.Entity<QualityDocument>().Property(e => e.ReviewCycleMonths).IsRequired();

        // Deviations
        builder.Entity<Deviation>().HasKey(e => e.Id);
        builder.Entity<Deviation>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<DeviationId, int>(id => id.Value, value => new DeviationId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Deviation>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<Deviation>().Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Entity<Deviation>().Property(e => e.BatchCode).IsRequired().HasMaxLength(30);
        builder.Entity<Deviation>().Property(e => e.OrderCode).IsRequired().HasMaxLength(30);
        builder.Entity<Deviation>().Property(e => e.Summary).IsRequired().HasColumnType("text");
        builder.Entity<Deviation>().Property(e => e.Classification).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Deviation>().Property(e => e.Containment).IsRequired().HasColumnType("text");
        builder.Entity<Deviation>().Property(e => e.Severity).IsRequired();
        builder.Entity<Deviation>().Property(e => e.Likelihood).IsRequired();
        builder.Entity<Deviation>().Property(e => e.Detectability).IsRequired();
        builder.Entity<Deviation>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Deviation>().Property(e => e.RootCause).IsRequired().HasMaxLength(150);
        builder.Entity<Deviation>().Property(e => e.CapaCode).IsRequired().HasMaxLength(30);
        builder.Entity<Deviation>().Property(e => e.ReportedBy).IsRequired().HasMaxLength(100);
        builder.Entity<Deviation>().Property(e => e.ReportedAt);
        builder.Entity<Deviation>().Property(e => e.Owner).IsRequired().HasMaxLength(100);
        builder.Entity<Deviation>().Property(e => e.DueAt);
        builder.Entity<Deviation>().HasIndex(e => e.Code).IsUnique();
        builder.Entity<Deviation>().HasIndex(e => e.BatchCode);

        // Evidence
        builder.Entity<Evidence>().HasKey(e => e.Id);
        builder.Entity<Evidence>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<EvidenceId, int>(id => id.Value, value => new EvidenceId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Evidence>().Property(e => e.RecordCode).IsRequired().HasMaxLength(60);
        builder.Entity<Evidence>().Property(e => e.FileName).IsRequired().HasMaxLength(255);
        builder.Entity<Evidence>().Property(e => e.Detail).IsRequired().HasMaxLength(255);
        builder.Entity<Evidence>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Evidence>().HasIndex(e => e.RecordCode);

        // CapaPlans
        builder.Entity<CapaPlan>().HasKey(e => e.Id);
        builder.Entity<CapaPlan>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<CapaPlanId, int>(id => id.Value, value => new CapaPlanId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<CapaPlan>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<CapaPlan>().Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Entity<CapaPlan>().Property(e => e.SourceDeviation).IsRequired().HasMaxLength(30);
        builder.Entity<CapaPlan>().Property(e => e.Owner).IsRequired().HasMaxLength(100);
        builder.Entity<CapaPlan>().Property(e => e.Reviewer).IsRequired().HasMaxLength(100);
        builder.Entity<CapaPlan>().Property(e => e.RootCause).IsRequired().HasMaxLength(150);
        builder.Entity<CapaPlan>().Property(e => e.AcceptanceCriterion).IsRequired().HasMaxLength(500);
        builder.Entity<CapaPlan>().Property(e => e.EffectivenessCheckAt);
        builder.Entity<CapaPlan>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<CapaPlan>().HasIndex(e => e.Code).IsUnique();
        builder.Entity<CapaPlan>().HasIndex(e => e.SourceDeviation);

        // CapaActions
        builder.Entity<CapaAction>().HasKey(e => e.Id);
        builder.Entity<CapaAction>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<CapaActionId, int>(id => id.Value, value => new CapaActionId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<CapaAction>().Property(e => e.CapaCode).IsRequired().HasMaxLength(30);
        builder.Entity<CapaAction>().Property(e => e.Code).IsRequired().HasMaxLength(10);
        builder.Entity<CapaAction>().Property(e => e.Type).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<CapaAction>().Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Entity<CapaAction>().Property(e => e.Owner).IsRequired().HasMaxLength(100);
        builder.Entity<CapaAction>().Property(e => e.DueAt);
        builder.Entity<CapaAction>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<CapaAction>().Property(e => e.Evidence).IsRequired().HasMaxLength(150);
        builder.Entity<CapaAction>().Property(e => e.EvidenceAttached).IsRequired();
        builder.Entity<CapaAction>().HasIndex(e => e.CapaCode);

        // AnalyticalResults
        builder.Entity<AnalyticalResult>().HasKey(e => e.Id);
        builder.Entity<AnalyticalResult>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AnalyticalResultId, int>(id => id.Value, value => new AnalyticalResultId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AnalyticalResult>().Property(e => e.BatchCode).IsRequired().HasMaxLength(30);
        builder.Entity<AnalyticalResult>().Property(e => e.SampleCode).IsRequired().HasMaxLength(30);
        builder.Entity<AnalyticalResult>().Property(e => e.ReportCode).IsRequired().HasMaxLength(30);
        builder.Entity<AnalyticalResult>().Property(e => e.Test).IsRequired().HasMaxLength(100).HasColumnName("test_name");
        builder.Entity<AnalyticalResult>().Property(e => e.Protocol).IsRequired().HasMaxLength(50);
        builder.Entity<AnalyticalResult>().Property(e => e.Result).IsRequired().HasMaxLength(30);
        builder.Entity<AnalyticalResult>().Property(e => e.Specification).IsRequired().HasMaxLength(50);
        builder.Entity<AnalyticalResult>().Property(e => e.Analyst).IsRequired().HasMaxLength(100);
        builder.Entity<AnalyticalResult>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<AnalyticalResult>().Property(e => e.Approval).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<AnalyticalResult>().HasIndex(e => e.BatchCode);

        // Audits
        builder.Entity<Audit>().HasKey(e => e.Id);
        builder.Entity<Audit>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AuditId, int>(id => id.Value, value => new AuditId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Audit>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<Audit>().Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Entity<Audit>().Property(e => e.Period).IsRequired().HasMaxLength(40);
        builder.Entity<Audit>().Property(e => e.AreasReviewed).IsRequired();
        builder.Entity<Audit>().Property(e => e.AreasTotal).IsRequired();
        builder.Entity<Audit>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<Audit>().HasIndex(e => e.Code).IsUnique();

        // AuditFindings
        builder.Entity<AuditFinding>().HasKey(e => e.Id);
        builder.Entity<AuditFinding>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AuditFindingId, int>(id => id.Value, value => new AuditFindingId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AuditFinding>().Property(e => e.AuditCode).IsRequired().HasMaxLength(30);
        builder.Entity<AuditFinding>().Property(e => e.Code).IsRequired().HasMaxLength(10);
        builder.Entity<AuditFinding>().Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Entity<AuditFinding>().Property(e => e.Classification).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<AuditFinding>().Property(e => e.Owner).IsRequired().HasMaxLength(100);
        builder.Entity<AuditFinding>().Property(e => e.DueAt);
        builder.Entity<AuditFinding>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<AuditFinding>().Property(e => e.Observation).IsRequired().HasColumnType("text");
        builder.Entity<AuditFinding>().Property(e => e.Response).IsRequired().HasColumnType("text");
        builder.Entity<AuditFinding>().Property(e => e.ClosureRequirement).IsRequired().HasColumnType("text");
        builder.Entity<AuditFinding>().HasIndex(e => e.AuditCode);

        // AuditEvents
        builder.Entity<AuditEvent>().HasKey(e => e.Id);
        builder.Entity<AuditEvent>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<AuditEventId, int>(id => id.Value, value => new AuditEventId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AuditEvent>().Property(e => e.EventCode).IsRequired().HasMaxLength(30);
        builder.Entity<AuditEvent>().Property(e => e.OccurredAt);
        builder.Entity<AuditEvent>().Property(e => e.Actor).IsRequired().HasMaxLength(100);
        builder.Entity<AuditEvent>().Property(e => e.ActorRole).IsRequired().HasMaxLength(40);
        builder.Entity<AuditEvent>().Property(e => e.Event).IsRequired().HasMaxLength(100);
        builder.Entity<AuditEvent>().Property(e => e.Record).IsRequired().HasMaxLength(60).HasColumnName("record_code");
        builder.Entity<AuditEvent>().Property(e => e.Result).IsRequired().HasMaxLength(40);
        builder.Entity<AuditEvent>().Property(e => e.Field).IsRequired().HasMaxLength(100);
        builder.Entity<AuditEvent>().Property(e => e.Before).IsRequired().HasMaxLength(255).HasColumnName("before_value");
        builder.Entity<AuditEvent>().Property(e => e.After).IsRequired().HasMaxLength(255).HasColumnName("after_value");
        builder.Entity<AuditEvent>().Property(e => e.Reason).IsRequired().HasMaxLength(255);
        builder.Entity<AuditEvent>().HasIndex(e => e.Record);

        // RegulatoryReports
        builder.Entity<RegulatoryReport>().HasKey(e => e.Id);
        builder.Entity<RegulatoryReport>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<RegulatoryReportId, int>(id => id.Value, value => new RegulatoryReportId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<RegulatoryReport>().Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Entity<RegulatoryReport>().Property(e => e.Type).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<RegulatoryReport>().Property(e => e.Scope).IsRequired().HasMaxLength(100);
        builder.Entity<RegulatoryReport>().Property(e => e.Owner).IsRequired().HasMaxLength(100);
        builder.Entity<RegulatoryReport>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<RegulatoryReport>().Property(e => e.Template).IsRequired().HasMaxLength(100);
        builder.Entity<RegulatoryReport>().Property(e => e.Format).IsRequired().HasMaxLength(100);
        builder.Entity<RegulatoryReport>().Property(e => e.CreatedOn).HasColumnName("created_on");
        builder.Entity<RegulatoryReport>().HasIndex(e => e.Code).IsUnique();

        // CollaborationTasks
        builder.Entity<CollaborationTask>().HasKey(e => e.Id);
        builder.Entity<CollaborationTask>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<CollaborationTaskId, int>(id => id.Value, value => new CollaborationTaskId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<CollaborationTask>().Property(e => e.Title).IsRequired().HasMaxLength(255);
        builder.Entity<CollaborationTask>().Property(e => e.RecordCode).IsRequired().HasMaxLength(60);
        builder.Entity<CollaborationTask>().Property(e => e.Detail).IsRequired().HasMaxLength(255);
        builder.Entity<CollaborationTask>().Property(e => e.Owner).IsRequired().HasMaxLength(100);
        builder.Entity<CollaborationTask>().Property(e => e.DueAt);
        builder.Entity<CollaborationTask>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<CollaborationTask>().HasIndex(e => e.RecordCode);

        // TaskComments
        builder.Entity<TaskComment>().HasKey(e => e.Id);
        builder.Entity<TaskComment>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<TaskCommentId, int>(id => id.Value, value => new TaskCommentId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<TaskComment>().Property(e => e.RecordCode).IsRequired().HasMaxLength(60);
        builder.Entity<TaskComment>().Property(e => e.Author).IsRequired().HasMaxLength(100);
        builder.Entity<TaskComment>().Property(e => e.Initials).IsRequired().HasMaxLength(5);
        builder.Entity<TaskComment>().Property(e => e.PostedAt);
        builder.Entity<TaskComment>().Property(e => e.Text).IsRequired().HasColumnType("text");
        builder.Entity<TaskComment>().HasIndex(e => e.RecordCode);

        // TraceabilityGaps
        builder.Entity<TraceabilityGap>().HasKey(e => e.Id);
        builder.Entity<TraceabilityGap>().Property(e => e.Id)
            .HasConversion(new StronglyTypedIdConverter<TraceabilityGapId, int>(id => id.Value, value => new TraceabilityGapId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<TraceabilityGap>().Property(e => e.BatchCode).IsRequired().HasMaxLength(30);
        builder.Entity<TraceabilityGap>().Property(e => e.Product).IsRequired().HasMaxLength(100);
        builder.Entity<TraceabilityGap>().Property(e => e.MissingRecord).IsRequired().HasMaxLength(150);
        builder.Entity<TraceabilityGap>().Property(e => e.Detail).IsRequired().HasMaxLength(500);
        builder.Entity<TraceabilityGap>().Property(e => e.Status).HasConversion<string>().HasMaxLength(24).IsRequired();
        builder.Entity<TraceabilityGap>().HasIndex(e => e.BatchCode);

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

    }
}
