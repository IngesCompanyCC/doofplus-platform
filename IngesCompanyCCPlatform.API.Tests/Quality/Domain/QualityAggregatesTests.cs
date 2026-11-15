using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Quality.Domain;

/// <summary>
///     Tests of the aggregates of Quality &amp; Compliance built from <c>contratos/quality-api.json</c>.
/// </summary>
public class QualityAggregatesTests
{
    [Fact]
    public void AQualityDocumentShouldKeepTheDataOfItsCommand()
    {
        var entity = new QualityDocument(new CreateQualityDocumentCommand("X-26001", "VERSION-1", "TITLE-1", EDocumentStatus.Obsolete, "AUTHOR-1", "TECHNICALREVIEWER-1", "APPROVER-1", new DateOnly(2026, 11, 15), "PURPOSE-1", "SCOPE-1", "PROCEDURE-1", "REVISIONSUMMARY-1", 3));

        Assert.Equal("X-26001", entity.Code);
        Assert.Equal("VERSION-1", entity.Version);
        Assert.Equal("TITLE-1", entity.Title);
    }

    [Fact]
    public void AQualityDocumentWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new QualityDocument(new CreateQualityDocumentCommand(" ", "VERSION-1", "TITLE-1", EDocumentStatus.Obsolete, "AUTHOR-1", "TECHNICALREVIEWER-1", "APPROVER-1", new DateOnly(2026, 11, 15), "PURPOSE-1", "SCOPE-1", "PROCEDURE-1", "REVISIONSUMMARY-1", 3)));
    }

    [Fact]
    public void ADeviationShouldKeepTheDataOfItsCommand()
    {
        var entity = new Deviation(new CreateDeviationCommand("X-26001", "TITLE-1", "BATCHCODE-1", "ORDERCODE-1", "SUMMARY-1", EClassification.Critical, "CONTAINMENT-1", 3, 3, 3, EDeviationStatus.Closed, "ROOTCAUSE-1", "CAPACODE-1", "REPORTEDBY-1", new DateTime(2026, 11, 15, 10, 0, 0, DateTimeKind.Utc), "OWNER-1", new DateTime(2026, 11, 15, 10, 0, 0, DateTimeKind.Utc)));

        Assert.Equal("X-26001", entity.Code);
        Assert.Equal("TITLE-1", entity.Title);
    }

    [Fact]
    public void ADeviationWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new Deviation(new CreateDeviationCommand(" ", "TITLE-1", "BATCHCODE-1", "ORDERCODE-1", "SUMMARY-1", EClassification.Critical, "CONTAINMENT-1", 3, 3, 3, EDeviationStatus.Closed, "ROOTCAUSE-1", "CAPACODE-1", "REPORTEDBY-1", new DateTime(2026, 11, 15, 10, 0, 0, DateTimeKind.Utc), "OWNER-1", new DateTime(2026, 11, 15, 10, 0, 0, DateTimeKind.Utc))));
    }

    [Fact]
    public void AEvidenceShouldKeepTheDataOfItsCommand()
    {
        var entity = new Evidence(new CreateEvidenceCommand("RECORDCODE-1", "FILENAME-1", "DETAIL-1", EEvidenceStatus.Reviewed));

        Assert.Equal("RECORDCODE-1", entity.RecordCode);
        Assert.Equal("FILENAME-1", entity.FileName);
    }

    [Fact]
    public void AEvidenceWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new Evidence(new CreateEvidenceCommand(" ", "FILENAME-1", "DETAIL-1", EEvidenceStatus.Reviewed)));
    }

    [Fact]
    public void ACapaPlanShouldKeepTheDataOfItsCommand()
    {
        var entity = new CapaPlan(new CreateCapaPlanCommand("X-26001", "TITLE-1", "SOURCEDEVIATION-1", "OWNER-1", "REVIEWER-1", "ROOTCAUSE-1", "ACCEPTANCECRITERION-1", new DateOnly(2026, 11, 15), ECapaStatus.Closed));

        Assert.Equal("X-26001", entity.Code);
        Assert.Equal("TITLE-1", entity.Title);
    }

    [Fact]
    public void ACapaPlanWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new CapaPlan(new CreateCapaPlanCommand(" ", "TITLE-1", "SOURCEDEVIATION-1", "OWNER-1", "REVIEWER-1", "ROOTCAUSE-1", "ACCEPTANCECRITERION-1", new DateOnly(2026, 11, 15), ECapaStatus.Closed)));
    }

    [Fact]
    public void ACapaActionShouldKeepTheDataOfItsCommand()
    {
        var entity = new CapaAction(new CreateCapaActionCommand("CAPACODE-1", "PA-01", null, "TITLE-1", "OWNER-1", new DateOnly(2026, 11, 15), EActionStatus.Complete, "EVIDENCE-1", true));

        Assert.Equal("CAPACODE-1", entity.CapaCode);
        Assert.Equal("PA-01", entity.Code);
        Assert.Equal("TITLE-1", entity.Title);
        Assert.Equal(ECapaType.Preventive, entity.Type);
    }

    [Fact]
    public void ACapaActionWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new CapaAction(new CreateCapaActionCommand(" ", "PA-01", null, "TITLE-1", "OWNER-1", new DateOnly(2026, 11, 15), EActionStatus.Complete, "EVIDENCE-1", true)));
    }

    [Fact]
    public void AAnalyticalResultShouldKeepTheDataOfItsCommand()
    {
        var entity = new AnalyticalResult(new CreateAnalyticalResultCommand("BATCHCODE-1", "SAMPLECODE-1", "REPORTCODE-1", "TEST-1", "PROTOCOL-1", "RESULT-1", "SPECIFICATION-1", "ANALYST-1", EResultStatus.Pending, EApprovalState.Approved));

        Assert.Equal("BATCHCODE-1", entity.BatchCode);
        Assert.Equal("TEST-1", entity.Test);
    }

    [Fact]
    public void AAnalyticalResultWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new AnalyticalResult(new CreateAnalyticalResultCommand(" ", "SAMPLECODE-1", "REPORTCODE-1", "TEST-1", "PROTOCOL-1", "RESULT-1", "SPECIFICATION-1", "ANALYST-1", EResultStatus.Pending, EApprovalState.Approved)));
    }

    [Fact]
    public void AAuditShouldKeepTheDataOfItsCommand()
    {
        var entity = new Audit(new CreateAuditCommand("X-26001", "TITLE-1", "PERIOD-1", 3, 3, EAuditStatus.Closed));

        Assert.Equal("X-26001", entity.Code);
        Assert.Equal("TITLE-1", entity.Title);
    }

    [Fact]
    public void AAuditWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new Audit(new CreateAuditCommand(" ", "TITLE-1", "PERIOD-1", 3, 3, EAuditStatus.Closed)));
    }

    [Fact]
    public void AAuditFindingShouldKeepTheDataOfItsCommand()
    {
        var entity = new AuditFinding(new CreateAuditFindingCommand("AUDITCODE-1", "X-26001", "TITLE-1", EClassification.Critical, "OWNER-1", new DateOnly(2026, 11, 15), EFindingStatus.Closed, "OBSERVATION-1", "RESPONSE-1", "CLOSUREREQUIREMENT-1"));

        Assert.Equal("AUDITCODE-1", entity.AuditCode);
        Assert.Equal("X-26001", entity.Code);
        Assert.Equal("TITLE-1", entity.Title);
    }

    [Fact]
    public void AAuditFindingWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new AuditFinding(new CreateAuditFindingCommand(" ", "X-26001", "TITLE-1", EClassification.Critical, "OWNER-1", new DateOnly(2026, 11, 15), EFindingStatus.Closed, "OBSERVATION-1", "RESPONSE-1", "CLOSUREREQUIREMENT-1")));
    }

    [Fact]
    public void AAuditEventShouldKeepTheDataOfItsCommand()
    {
        var entity = new AuditEvent(new CreateAuditEventCommand("EVENTCODE-1", null, "ACTOR-1", "ACTORROLE-1", "EVENT-1", "RECORD-1", "RESULT-1", "FIELD-1", "BEFORE-1", "AFTER-1", "REASON-1"));

        Assert.Equal("EVENTCODE-1", entity.EventCode);
        Assert.Equal("EVENT-1", entity.Event);
        Assert.NotNull(entity.OccurredAt);
    }

    [Fact]
    public void AAuditEventWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new AuditEvent(new CreateAuditEventCommand(" ", null, "ACTOR-1", "ACTORROLE-1", "EVENT-1", "RECORD-1", "RESULT-1", "FIELD-1", "BEFORE-1", "AFTER-1", "REASON-1")));
    }

    [Fact]
    public void ARegulatoryReportShouldKeepTheDataOfItsCommand()
    {
        var entity = new RegulatoryReport(new CreateRegulatoryReportCommand("X-26001", EReportType.AuditEvidence, "SCOPE-1", "OWNER-1", EReportStatus.Approved, "TEMPLATE-1", "FORMAT-1", new DateOnly(2026, 11, 15)));

        Assert.Equal("X-26001", entity.Code);
        Assert.NotNull(entity.CreatedOn);
    }

    [Fact]
    public void ARegulatoryReportWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new RegulatoryReport(new CreateRegulatoryReportCommand(" ", EReportType.AuditEvidence, "SCOPE-1", "OWNER-1", EReportStatus.Approved, "TEMPLATE-1", "FORMAT-1", new DateOnly(2026, 11, 15))));
    }

    [Fact]
    public void ACollaborationTaskShouldKeepTheDataOfItsCommand()
    {
        var entity = new CollaborationTask(new CreateCollaborationTaskCommand("TITLE-1", "RECORDCODE-1", "DETAIL-1", "OWNER-1", new DateTime(2026, 11, 15, 10, 0, 0, DateTimeKind.Utc), ETaskStatus.Done));

        Assert.Equal("TITLE-1", entity.Title);
    }

    [Fact]
    public void ACollaborationTaskWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new CollaborationTask(new CreateCollaborationTaskCommand(" ", "RECORDCODE-1", "DETAIL-1", "OWNER-1", new DateTime(2026, 11, 15, 10, 0, 0, DateTimeKind.Utc), ETaskStatus.Done)));
    }

    [Fact]
    public void ATaskCommentShouldKeepTheDataOfItsCommand()
    {
        var entity = new TaskComment(new CreateTaskCommentCommand("RECORDCODE-1", "AUTHOR-1", "INITIALS-1", null, "TEXT-1"));

        Assert.Equal("RECORDCODE-1", entity.RecordCode);
        Assert.Equal("TEXT-1", entity.Text);
        Assert.NotNull(entity.PostedAt);
    }

    [Fact]
    public void ATaskCommentWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new TaskComment(new CreateTaskCommentCommand(" ", "AUTHOR-1", "INITIALS-1", null, "TEXT-1")));
    }

    [Fact]
    public void ATraceabilityGapShouldKeepTheDataOfItsCommand()
    {
        var entity = new TraceabilityGap(new CreateTraceabilityGapCommand("BATCHCODE-1", "PRODUCT-1", "MISSINGRECORD-1", "DETAIL-1", EGapStatus.Resolved));

        Assert.Equal("BATCHCODE-1", entity.BatchCode);
        Assert.Equal("MISSINGRECORD-1", entity.MissingRecord);
    }

    [Fact]
    public void ATraceabilityGapWithoutRequiredDataShouldNotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new TraceabilityGap(new CreateTraceabilityGapCommand(" ", "PRODUCT-1", "MISSINGRECORD-1", "DETAIL-1", EGapStatus.Resolved)));
    }

    [Fact]
    public void ReviewedEvidenceShouldNotBeRemovable()
    {
        var reviewed = new Evidence(new CreateEvidenceCommand("DEV-26017", "Telemetry.csv", "", EEvidenceStatus.Reviewed));
        var attached = new Evidence(new CreateEvidenceCommand("DEV-26017", "Telemetry.csv", "", EEvidenceStatus.Attached));

        Assert.False(reviewed.CanBeRemoved());
        Assert.True(attached.CanBeRemoved());
    }
}
