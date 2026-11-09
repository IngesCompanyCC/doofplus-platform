using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Entity Framework Core configuration of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     The tables follow the database diagram of the report (section 4.7.2). The references to other
///     bounded contexts (batches, raw material lots, users, signatures and organizations) only keep the id.
/// </remarks>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the mapping of the aggregates of the Quality &amp; Compliance bounded context.
    /// </summary>
    /// <param name="builder">The model builder of the database context.</param>
    public static void ApplyQualityConfiguration(this ModelBuilder builder)
    {
        ConfigureQualityDocuments(builder);
        ConfigureMaterialApprovals(builder);
        ConfigureAnalyticalResults(builder);
        ConfigureBatchReviews(builder);
        ConfigureDeviations(builder);
        ConfigureAudits(builder);
        ConfigureAuditTrailEntries(builder);
        ConfigureRegulatoryReports(builder);
    }

    private static StronglyTypedIdConverter<TId, int> IdConverter<TId>(Func<int, TId> factory)
        where TId : struct, IEntityId<int>
    {
        return new StronglyTypedIdConverter<TId, int>(id => id.Value, factory);
    }

    /// <summary>
    ///     Stores a <see cref="DateOnly" /> as a <c>date</c> column. The MySQL provider reads those
    ///     columns back as <see cref="DateTime" />, so the value goes through a converter.
    /// </summary>
    private static readonly ValueConverter<DateOnly, DateTime> DateOnlyConverter = new(
        date => date.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime));

    private static void ConfigureQualityDocuments(ModelBuilder builder)
    {
        builder.Entity<QualityDocument>().HasKey(d => d.Id);
        builder.Entity<QualityDocument>().Property(d => d.Id)
            .HasConversion(IdConverter(value => new QualityDocumentId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<QualityDocument>().Property(d => d.Code).IsRequired().HasMaxLength(30);
        builder.Entity<QualityDocument>().HasIndex(d => d.Code).IsUnique();
        builder.Entity<QualityDocument>().Property(d => d.Title).IsRequired().HasMaxLength(255);
        builder.Entity<QualityDocument>().Property(d => d.Type).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Entity<QualityDocument>().Property(d => d.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<QualityDocument>().Ignore(d => d.CurrentVersion);

        builder.Entity<DocumentVersion>().HasKey(v => v.Id);
        builder.Entity<DocumentVersion>().Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<DocumentVersion>().Property<QualityDocumentId>("QualityDocumentId")
            .HasConversion(IdConverter(value => new QualityDocumentId(value)))
            .IsRequired();
        builder.Entity<DocumentVersion>().Property(v => v.VersionNumber).IsRequired().HasMaxLength(10);
        builder.Entity<DocumentVersion>().Property(v => v.FileUrl).IsRequired().HasMaxLength(500);
        builder.Entity<DocumentVersion>().Property(v => v.ChangeReason).IsRequired().HasMaxLength(500);
        builder.Entity<DocumentVersion>().Property(v => v.AuthorId)
            .HasConversion(IdConverter(value => new UserId(value)))
            .IsRequired();
        builder.Entity<DocumentVersion>().Property(v => v.ApprovalSignatureId)
            .HasConversion(IdConverter(value => new ElectronicSignatureId(value)));

        builder.Entity<QualityDocument>().HasMany(d => d.Versions)
            .WithOne()
            .HasForeignKey("QualityDocumentId")
            .IsRequired();
        builder.Entity<QualityDocument>().Navigation(d => d.Versions).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMaterialApprovals(ModelBuilder builder)
    {
        builder.Entity<MaterialApproval>().HasKey(m => m.Id);
        builder.Entity<MaterialApproval>().Property(m => m.Id)
            .HasConversion(IdConverter(value => new MaterialApprovalId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<MaterialApproval>().Property(m => m.RawMaterialLotId)
            .HasConversion(IdConverter(value => new RawMaterialLotId(value)))
            .IsRequired();
        builder.Entity<MaterialApproval>().HasIndex(m => m.RawMaterialLotId).IsUnique();
        builder.Entity<MaterialApproval>().Property(m => m.SampleCode).HasMaxLength(30);
        builder.Entity<MaterialApproval>().Property(m => m.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<MaterialApproval>().Property(m => m.DecidedBy)
            .HasConversion(IdConverter(value => new UserId(value)));
        builder.Entity<MaterialApproval>().Property(m => m.SignatureId)
            .HasConversion(IdConverter(value => new ElectronicSignatureId(value)));
        builder.Entity<MaterialApproval>().Property(m => m.Reason).HasMaxLength(500);
    }

    private static void ConfigureAnalyticalResults(ModelBuilder builder)
    {
        builder.Entity<AnalyticalResult>().HasKey(r => r.Id);
        builder.Entity<AnalyticalResult>().Property(r => r.Id)
            .HasConversion(IdConverter(value => new AnalyticalResultId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AnalyticalResult>().Property(r => r.ProductionBatchId)
            .HasConversion(IdConverter(value => new ProductionBatchId(value)))
            .IsRequired();
        builder.Entity<AnalyticalResult>().HasIndex(r => r.ProductionBatchId);
        builder.Entity<AnalyticalResult>().Property(r => r.TestName).IsRequired().HasMaxLength(150);
        builder.Entity<AnalyticalResult>().Property(r => r.Value).HasPrecision(12, 4).IsRequired();
        builder.Entity<AnalyticalResult>().ComplexProperty(r => r.Specification, specification =>
        {
            specification.Property(s => s.Min).HasColumnName("spec_min").HasPrecision(12, 4).IsRequired();
            specification.Property(s => s.Max).HasColumnName("spec_max").HasPrecision(12, 4).IsRequired();
            specification.Property(s => s.Unit).HasColumnName("unit").HasMaxLength(10).IsRequired();
        });
        builder.Entity<AnalyticalResult>().Property(r => r.OutOfSpecification).IsRequired();
        builder.Entity<AnalyticalResult>().Property(r => r.AnalystId)
            .HasConversion(IdConverter(value => new UserId(value)))
            .IsRequired();
    }

    private static void ConfigureBatchReviews(ModelBuilder builder)
    {
        builder.Entity<BatchReview>().HasKey(b => b.Id);
        builder.Entity<BatchReview>().Property(b => b.Id)
            .HasConversion(IdConverter(value => new BatchReviewId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<BatchReview>().Property(b => b.ProductionBatchId)
            .HasConversion(IdConverter(value => new ProductionBatchId(value)))
            .IsRequired();
        builder.Entity<BatchReview>().HasIndex(b => b.ProductionBatchId).IsUnique();
        builder.Entity<BatchReview>().Property(b => b.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<BatchReview>().Property(b => b.QuarantineStartedAt).IsRequired();
        builder.Entity<BatchReview>().Property(b => b.SampleCode).HasMaxLength(30);
        builder.Entity<BatchReview>().Property(b => b.EvaluatedAt);
        builder.Entity<BatchReview>().Property(b => b.ReleaseBlocked).IsRequired();
        builder.Entity<BatchReview>().Property(b => b.RejectionReason).HasMaxLength(500);

        builder.Entity<EvidenceAttachment>().HasKey(e => e.Id);
        builder.Entity<EvidenceAttachment>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<EvidenceAttachment>().Property<BatchReviewId>("BatchReviewId")
            .HasConversion(IdConverter(value => new BatchReviewId(value)))
            .IsRequired();
        builder.Entity<EvidenceAttachment>().Property(e => e.FileUrl).IsRequired().HasMaxLength(500);
        builder.Entity<EvidenceAttachment>().Property(e => e.Description).HasMaxLength(255);
        builder.Entity<BatchReview>().HasMany(b => b.Evidences)
            .WithOne()
            .HasForeignKey("BatchReviewId")
            .IsRequired();
        builder.Entity<BatchReview>().Navigation(b => b.Evidences).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Entity<ReleaseCertificate>().HasKey(c => c.Id);
        builder.Entity<ReleaseCertificate>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ReleaseCertificate>().Property<BatchReviewId>("BatchReviewId")
            .HasConversion(IdConverter(value => new BatchReviewId(value)))
            .IsRequired();
        builder.Entity<ReleaseCertificate>().Property(c => c.CertificateNumber).IsRequired().HasMaxLength(30);
        builder.Entity<ReleaseCertificate>().HasIndex(c => c.CertificateNumber).IsUnique();
        builder.Entity<ReleaseCertificate>().Property(c => c.Verdict).HasConversion<string>().HasMaxLength(10)
            .IsRequired();
        builder.Entity<ReleaseCertificate>().Property(c => c.SignatureId)
            .HasConversion(IdConverter(value => new ElectronicSignatureId(value)))
            .IsRequired();
        builder.Entity<ReleaseCertificate>().Property(c => c.IssuedAt).IsRequired();
        builder.Entity<BatchReview>().HasOne(b => b.Certificate)
            .WithOne()
            .HasForeignKey<ReleaseCertificate>("BatchReviewId")
            .IsRequired();
    }

    private static void ConfigureDeviations(ModelBuilder builder)
    {
        builder.Entity<Deviation>().HasKey(d => d.Id);
        builder.Entity<Deviation>().Property(d => d.Id)
            .HasConversion(IdConverter(value => new DeviationId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Deviation>().Property(d => d.Code).IsRequired().HasMaxLength(30);
        builder.Entity<Deviation>().HasIndex(d => d.Code).IsUnique();
        builder.Entity<Deviation>().Property(d => d.ProductionBatchId)
            .HasConversion(IdConverter(value => new ProductionBatchId(value)))
            .IsRequired();
        builder.Entity<Deviation>().HasIndex(d => d.ProductionBatchId);
        builder.Entity<Deviation>().Property(d => d.Severity).HasConversion<string>().HasMaxLength(10).IsRequired();
        builder.Entity<Deviation>().Property(d => d.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<Deviation>().Property(d => d.Description).IsRequired();
        builder.Entity<Deviation>().OwnsOne(d => d.RootCause, rootCause =>
        {
            // The owned type shares the table and the key of the deviation
            rootCause.Property<DeviationId>("DeviationId").HasColumnName("id");
            rootCause.Property(r => r.Method).HasColumnName("root_cause_method").HasMaxLength(50);
            rootCause.Property(r => r.Description).HasColumnName("root_cause_description");
        });
        builder.Entity<Deviation>().Property(d => d.DetectedAt).IsRequired();
        builder.Entity<Deviation>().Property(d => d.ClosedAt);

        builder.Entity<CapaAction>().HasKey(a => a.Id);
        builder.Entity<CapaAction>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CapaAction>().Property<DeviationId>("DeviationId")
            .HasConversion(IdConverter(value => new DeviationId(value)))
            .IsRequired();
        builder.Entity<CapaAction>().Property(a => a.Type).HasConversion<string>().HasMaxLength(12).IsRequired();
        builder.Entity<CapaAction>().Property(a => a.Description).IsRequired().HasMaxLength(500);
        builder.Entity<CapaAction>().Property(a => a.ResponsibleId)
            .HasConversion(IdConverter(value => new UserId(value)))
            .IsRequired();
        builder.Entity<CapaAction>().Property(a => a.DueDate).IsRequired().HasConversion(DateOnlyConverter).HasColumnType("date");
        builder.Entity<CapaAction>().Property(a => a.Progress).IsRequired();
        builder.Entity<CapaAction>().Property(a => a.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<Deviation>().HasMany(d => d.Actions)
            .WithOne()
            .HasForeignKey("DeviationId")
            .IsRequired();
        builder.Entity<Deviation>().Navigation(d => d.Actions).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureAudits(ModelBuilder builder)
    {
        builder.Entity<Audit>().HasKey(a => a.Id);
        builder.Entity<Audit>().Property(a => a.Id)
            .HasConversion(IdConverter(value => new AuditId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Audit>().Property(a => a.Type).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<Audit>().Property(a => a.ScheduledDate).IsRequired().HasConversion(DateOnlyConverter).HasColumnType("date");
        builder.Entity<Audit>().Property(a => a.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<Audit>().Property(a => a.EvidencePackageUrl).HasMaxLength(500);

        builder.Entity<AuditFinding>().HasKey(f => f.Id);
        builder.Entity<AuditFinding>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<AuditFinding>().Property<AuditId>("AuditId")
            .HasConversion(IdConverter(value => new AuditId(value)))
            .IsRequired();
        builder.Entity<AuditFinding>().Property(f => f.Type).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<AuditFinding>().Property(f => f.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Audit>().HasMany(a => a.Findings)
            .WithOne()
            .HasForeignKey("AuditId")
            .IsRequired();
        builder.Entity<Audit>().Navigation(a => a.Findings).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureAuditTrailEntries(ModelBuilder builder)
    {
        builder.Entity<AuditTrailEntry>().HasKey(e => e.Id);
        builder.Entity<AuditTrailEntry>().Property(e => e.Id)
            .HasConversion(IdConverter(value => new AuditTrailEntryId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<AuditTrailEntry>().Property(e => e.EntityType).IsRequired().HasMaxLength(100);
        builder.Entity<AuditTrailEntry>().Property(e => e.EntityId).IsRequired().HasMaxLength(36);
        builder.Entity<AuditTrailEntry>().HasIndex(e => new { e.EntityType, e.EntityId });
        builder.Entity<AuditTrailEntry>().Property(e => e.Action).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Entity<AuditTrailEntry>().Property(e => e.OldValue).HasColumnType("json");
        builder.Entity<AuditTrailEntry>().Property(e => e.NewValue).HasColumnType("json");
        builder.Entity<AuditTrailEntry>().Property(e => e.ChangedBy)
            .HasConversion(IdConverter(value => new UserId(value)))
            .IsRequired();
        builder.Entity<AuditTrailEntry>().Property(e => e.ChangedAt).IsRequired();
        builder.Entity<AuditTrailEntry>().Property(e => e.Reason).HasMaxLength(255);
    }

    private static void ConfigureRegulatoryReports(ModelBuilder builder)
    {
        builder.Entity<RegulatoryReport>().HasKey(r => r.Id);
        builder.Entity<RegulatoryReport>().Property(r => r.Id)
            .HasConversion(IdConverter(value => new RegulatoryReportId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<RegulatoryReport>().Property(r => r.OrganizationId)
            .HasConversion(IdConverter(value => new OrganizationId(value)))
            .IsRequired();
        builder.Entity<RegulatoryReport>().HasIndex(r => r.OrganizationId);
        builder.Entity<RegulatoryReport>().Property(r => r.Period)
            .HasConversion(period => period.Value, value => new ReportPeriod(value))
            .HasColumnType("char(7)")
            .IsRequired();
        builder.Entity<RegulatoryReport>().Property(r => r.FileUrl).IsRequired().HasMaxLength(500);
        builder.Entity<RegulatoryReport>().Property(r => r.GeneratedAt).IsRequired();
    }
}
