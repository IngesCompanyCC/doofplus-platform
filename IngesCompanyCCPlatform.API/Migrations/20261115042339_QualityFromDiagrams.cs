using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class QualityFromDiagrams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "i_x_traceability_gaps_batch_id",
                table: "traceability_gaps");

            migrationBuilder.DropIndex(
                name: "i_x_task_comments_task_id",
                table: "task_comments");

            migrationBuilder.DropIndex(
                name: "i_x_quality_documents_code",
                table: "quality_documents");

            migrationBuilder.DropIndex(
                name: "i_x_evidences_deviation_id",
                table: "evidences");

            migrationBuilder.DropIndex(
                name: "i_x_deviations_batch_id",
                table: "deviations");

            migrationBuilder.DropIndex(
                name: "i_x_capa_actions_capa_plan_id",
                table: "capa_actions");

            migrationBuilder.DropIndex(
                name: "i_x_audit_findings_audit_id",
                table: "audit_findings");

            migrationBuilder.DropIndex(
                name: "i_x_audit_events_aggregate_id",
                table: "audit_events");

            migrationBuilder.DropIndex(
                name: "i_x_audit_events_aggregate_type",
                table: "audit_events");

            migrationBuilder.DropIndex(
                name: "i_x_analytical_results_batch_id",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "affected_process",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "batch_id",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "description",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "detected_at",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "resolved_at",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "reviewed_by",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "author_id",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "content",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "task_id",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "description",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "file_reference",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "prepared_by",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "submitted_at",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "title",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "approved_at",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "description",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "document_type",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "file_reference",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "description",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "deviation_id",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "document_id",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "title",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "type",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "uploaded_at",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "uploaded_by",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "verified_at",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "assigned_to",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "description",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "detected_at",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "assigned_to",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "description",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "due_date",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "related_deviation_id",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "description",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "deviation_id",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "due_date",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "capa_plan_id",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "description",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "due_date",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "audit_type",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "auditor_id",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "planned_date",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "scope",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "started_at",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "audit_id",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "description",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "identified_at",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "related_deviation_id",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "aggregate_id",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "aggregate_type",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "description",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "batch_id",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "maximum_value",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "measured_value",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "minimum_value",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "reviewed_at",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "reviewed_by",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "tested_at",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "unit",
                table: "analytical_results");

            migrationBuilder.RenameColumn(
                name: "missing_reference",
                table: "traceability_gaps",
                newName: "product");

            migrationBuilder.RenameColumn(
                name: "regulatory_authority",
                table: "regulatory_reports",
                newName: "template");

            migrationBuilder.RenameColumn(
                name: "due_date",
                table: "regulatory_reports",
                newName: "created_on");

            migrationBuilder.RenameColumn(
                name: "file_reference",
                table: "evidences",
                newName: "file_name");

            migrationBuilder.RenameColumn(
                name: "closed_at",
                table: "deviations",
                newName: "reported_at");

            migrationBuilder.RenameColumn(
                name: "batch_id",
                table: "deviations",
                newName: "likelihood");

            migrationBuilder.RenameColumn(
                name: "completed_at",
                table: "collaboration_tasks",
                newName: "due_at");

            migrationBuilder.RenameColumn(
                name: "responsible",
                table: "capa_plans",
                newName: "reviewer");

            migrationBuilder.RenameColumn(
                name: "assigned_to",
                table: "capa_actions",
                newName: "owner");

            migrationBuilder.RenameColumn(
                name: "severity",
                table: "audit_findings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "performed_by",
                table: "audit_events",
                newName: "field");

            migrationBuilder.RenameColumn(
                name: "event_type",
                table: "audit_events",
                newName: "record_code");

            migrationBuilder.RenameColumn(
                name: "parameter",
                table: "analytical_results",
                newName: "analyst");

            migrationBuilder.AddColumn<string>(
                name: "batch_code",
                table: "traceability_gaps",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "detail",
                table: "traceability_gaps",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "missing_record",
                table: "traceability_gaps",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "author",
                table: "task_comments",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "initials",
                table: "task_comments",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "posted_at",
                table: "task_comments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "record_code",
                table: "task_comments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "task_comments",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "format",
                table: "regulatory_reports",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "regulatory_reports",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "scope",
                table: "regulatory_reports",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "regulatory_reports",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "version",
                table: "quality_documents",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "quality_documents",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "approver",
                table: "quality_documents",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "author",
                table: "quality_documents",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "changed_at",
                table: "quality_documents",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "procedure_text",
                table: "quality_documents",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "purpose",
                table: "quality_documents",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "review_cycle_months",
                table: "quality_documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "revision_summary",
                table: "quality_documents",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "scope",
                table: "quality_documents",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "technical_reviewer",
                table: "quality_documents",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "detail",
                table: "evidences",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "record_code",
                table: "evidences",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "deviations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "severity",
                table: "deviations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(24)",
                oldMaxLength: 24);

            migrationBuilder.AddColumn<string>(
                name: "batch_code",
                table: "deviations",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "capa_code",
                table: "deviations",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "classification",
                table: "deviations",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "containment",
                table: "deviations",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "detectability",
                table: "deviations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "due_at",
                table: "deviations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "order_code",
                table: "deviations",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "deviations",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reported_by",
                table: "deviations",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "root_cause",
                table: "deviations",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "deviations",
                type: "text",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "collaboration_tasks",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "detail",
                table: "collaboration_tasks",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "collaboration_tasks",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "record_code",
                table: "collaboration_tasks",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "capa_plans",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "acceptance_criterion",
                table: "capa_plans",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "capa_plans",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "effectiveness_check_at",
                table: "capa_plans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "capa_plans",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "root_cause",
                table: "capa_plans",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "source_deviation",
                table: "capa_plans",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "capa_actions",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "capa_code",
                table: "capa_actions",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "capa_actions",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "due_at",
                table: "capa_actions",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "evidence",
                table: "capa_actions",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "evidence_attached",
                table: "capa_actions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "capa_actions",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "audits",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "areas_reviewed",
                table: "audits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "areas_total",
                table: "audits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "period",
                table: "audits",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "audit_findings",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "audit_code",
                table: "audit_findings",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "classification",
                table: "audit_findings",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "closure_requirement",
                table: "audit_findings",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "audit_findings",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "due_at",
                table: "audit_findings",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "observation",
                table: "audit_findings",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "audit_findings",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "response",
                table: "audit_findings",
                type: "text",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "occurred_at",
                table: "audit_events",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "actor",
                table: "audit_events",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "actor_role",
                table: "audit_events",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "after_value",
                table: "audit_events",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "before_value",
                table: "audit_events",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "event",
                table: "audit_events",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "event_code",
                table: "audit_events",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "audit_events",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "result",
                table: "audit_events",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "approval",
                table: "analytical_results",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "batch_code",
                table: "analytical_results",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "protocol",
                table: "analytical_results",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "report_code",
                table: "analytical_results",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "result",
                table: "analytical_results",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sample_code",
                table: "analytical_results",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "specification",
                table: "analytical_results",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "i_x_traceability_gaps_batch_code",
                table: "traceability_gaps",
                column: "batch_code");

            migrationBuilder.CreateIndex(
                name: "i_x_task_comments_record_code",
                table: "task_comments",
                column: "record_code");

            migrationBuilder.CreateIndex(
                name: "i_x_evidences_record_code",
                table: "evidences",
                column: "record_code");

            migrationBuilder.CreateIndex(
                name: "i_x_deviations_batch_code",
                table: "deviations",
                column: "batch_code");

            migrationBuilder.CreateIndex(
                name: "i_x_collaboration_tasks_record_code",
                table: "collaboration_tasks",
                column: "record_code");

            migrationBuilder.CreateIndex(
                name: "i_x_capa_plans_code",
                table: "capa_plans",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_capa_plans_source_deviation",
                table: "capa_plans",
                column: "source_deviation");

            migrationBuilder.CreateIndex(
                name: "i_x_capa_actions_capa_code",
                table: "capa_actions",
                column: "capa_code");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_findings_audit_code",
                table: "audit_findings",
                column: "audit_code");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_events_record_code",
                table: "audit_events",
                column: "record_code");

            migrationBuilder.CreateIndex(
                name: "i_x_analytical_results_batch_code",
                table: "analytical_results",
                column: "batch_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "i_x_traceability_gaps_batch_code",
                table: "traceability_gaps");

            migrationBuilder.DropIndex(
                name: "i_x_task_comments_record_code",
                table: "task_comments");

            migrationBuilder.DropIndex(
                name: "i_x_evidences_record_code",
                table: "evidences");

            migrationBuilder.DropIndex(
                name: "i_x_deviations_batch_code",
                table: "deviations");

            migrationBuilder.DropIndex(
                name: "i_x_collaboration_tasks_record_code",
                table: "collaboration_tasks");

            migrationBuilder.DropIndex(
                name: "i_x_capa_plans_code",
                table: "capa_plans");

            migrationBuilder.DropIndex(
                name: "i_x_capa_plans_source_deviation",
                table: "capa_plans");

            migrationBuilder.DropIndex(
                name: "i_x_capa_actions_capa_code",
                table: "capa_actions");

            migrationBuilder.DropIndex(
                name: "i_x_audit_findings_audit_code",
                table: "audit_findings");

            migrationBuilder.DropIndex(
                name: "i_x_audit_events_record_code",
                table: "audit_events");

            migrationBuilder.DropIndex(
                name: "i_x_analytical_results_batch_code",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "batch_code",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "detail",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "missing_record",
                table: "traceability_gaps");

            migrationBuilder.DropColumn(
                name: "author",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "initials",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "posted_at",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "record_code",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "text",
                table: "task_comments");

            migrationBuilder.DropColumn(
                name: "format",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "scope",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "type",
                table: "regulatory_reports");

            migrationBuilder.DropColumn(
                name: "approver",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "author",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "changed_at",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "procedure_text",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "purpose",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "review_cycle_months",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "revision_summary",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "scope",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "technical_reviewer",
                table: "quality_documents");

            migrationBuilder.DropColumn(
                name: "detail",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "record_code",
                table: "evidences");

            migrationBuilder.DropColumn(
                name: "batch_code",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "capa_code",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "classification",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "containment",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "detectability",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "due_at",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "order_code",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "reported_by",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "root_cause",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "summary",
                table: "deviations");

            migrationBuilder.DropColumn(
                name: "detail",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "record_code",
                table: "collaboration_tasks");

            migrationBuilder.DropColumn(
                name: "acceptance_criterion",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "code",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "effectiveness_check_at",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "root_cause",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "source_deviation",
                table: "capa_plans");

            migrationBuilder.DropColumn(
                name: "capa_code",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "code",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "due_at",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "evidence",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "evidence_attached",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "type",
                table: "capa_actions");

            migrationBuilder.DropColumn(
                name: "areas_reviewed",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "areas_total",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "period",
                table: "audits");

            migrationBuilder.DropColumn(
                name: "audit_code",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "classification",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "closure_requirement",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "code",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "due_at",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "observation",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "response",
                table: "audit_findings");

            migrationBuilder.DropColumn(
                name: "actor",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "actor_role",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "after_value",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "before_value",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "event",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "event_code",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "result",
                table: "audit_events");

            migrationBuilder.DropColumn(
                name: "approval",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "batch_code",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "protocol",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "report_code",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "result",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "sample_code",
                table: "analytical_results");

            migrationBuilder.DropColumn(
                name: "specification",
                table: "analytical_results");

            migrationBuilder.RenameColumn(
                name: "product",
                table: "traceability_gaps",
                newName: "missing_reference");

            migrationBuilder.RenameColumn(
                name: "template",
                table: "regulatory_reports",
                newName: "regulatory_authority");

            migrationBuilder.RenameColumn(
                name: "created_on",
                table: "regulatory_reports",
                newName: "due_date");

            migrationBuilder.RenameColumn(
                name: "file_name",
                table: "evidences",
                newName: "file_reference");

            migrationBuilder.RenameColumn(
                name: "reported_at",
                table: "deviations",
                newName: "closed_at");

            migrationBuilder.RenameColumn(
                name: "likelihood",
                table: "deviations",
                newName: "batch_id");

            migrationBuilder.RenameColumn(
                name: "due_at",
                table: "collaboration_tasks",
                newName: "completed_at");

            migrationBuilder.RenameColumn(
                name: "reviewer",
                table: "capa_plans",
                newName: "responsible");

            migrationBuilder.RenameColumn(
                name: "owner",
                table: "capa_actions",
                newName: "assigned_to");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "audit_findings",
                newName: "severity");

            migrationBuilder.RenameColumn(
                name: "record_code",
                table: "audit_events",
                newName: "event_type");

            migrationBuilder.RenameColumn(
                name: "field",
                table: "audit_events",
                newName: "performed_by");

            migrationBuilder.RenameColumn(
                name: "analyst",
                table: "analytical_results",
                newName: "parameter");

            migrationBuilder.AddColumn<string>(
                name: "affected_process",
                table: "traceability_gaps",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "batch_id",
                table: "traceability_gaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "traceability_gaps",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "detected_at",
                table: "traceability_gaps",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "resolved_at",
                table: "traceability_gaps",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reviewed_by",
                table: "traceability_gaps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "author_id",
                table: "task_comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "task_comments",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "task_id",
                table: "task_comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "regulatory_reports",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "file_reference",
                table: "regulatory_reports",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "prepared_by",
                table: "regulatory_reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "submitted_at",
                table: "regulatory_reports",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "regulatory_reports",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "version",
                table: "quality_documents",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "quality_documents",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "approved_at",
                table: "quality_documents",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "quality_documents",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "document_type",
                table: "quality_documents",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "file_reference",
                table: "quality_documents",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "owner_id",
                table: "quality_documents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "evidences",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "deviation_id",
                table: "evidences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "document_id",
                table: "evidences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "evidences",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "evidences",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "uploaded_at",
                table: "evidences",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "uploaded_by",
                table: "evidences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "verified_at",
                table: "evidences",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "deviations",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "severity",
                table: "deviations",
                type: "varchar(24)",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "assigned_to",
                table: "deviations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "deviations",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "detected_at",
                table: "deviations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "collaboration_tasks",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "assigned_to",
                table: "collaboration_tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by",
                table: "collaboration_tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "collaboration_tasks",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "due_date",
                table: "collaboration_tasks",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "related_deviation_id",
                table: "collaboration_tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "capa_plans",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_at",
                table: "capa_plans",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "capa_plans",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "deviation_id",
                table: "capa_plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "due_date",
                table: "capa_plans",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "capa_actions",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "capa_plan_id",
                table: "capa_actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_at",
                table: "capa_actions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "capa_actions",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "due_date",
                table: "capa_actions",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "audits",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "audit_type",
                table: "audits",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "auditor_id",
                table: "audits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_at",
                table: "audits",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "planned_date",
                table: "audits",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "scope",
                table: "audits",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "started_at",
                table: "audits",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "audit_findings",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "audit_id",
                table: "audit_findings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "audit_findings",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "identified_at",
                table: "audit_findings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "related_deviation_id",
                table: "audit_findings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "occurred_at",
                table: "audit_events",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "aggregate_id",
                table: "audit_events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "aggregate_type",
                table: "audit_events",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "audit_events",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "batch_id",
                table: "analytical_results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "maximum_value",
                table: "analytical_results",
                type: "decimal(14,4)",
                precision: 14,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "measured_value",
                table: "analytical_results",
                type: "decimal(14,4)",
                precision: 14,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "minimum_value",
                table: "analytical_results",
                type: "decimal(14,4)",
                precision: 14,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "reviewed_at",
                table: "analytical_results",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reviewed_by",
                table: "analytical_results",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "tested_at",
                table: "analytical_results",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unit",
                table: "analytical_results",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "i_x_traceability_gaps_batch_id",
                table: "traceability_gaps",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "i_x_task_comments_task_id",
                table: "task_comments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "i_x_quality_documents_code",
                table: "quality_documents",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_evidences_deviation_id",
                table: "evidences",
                column: "deviation_id");

            migrationBuilder.CreateIndex(
                name: "i_x_deviations_batch_id",
                table: "deviations",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "i_x_capa_actions_capa_plan_id",
                table: "capa_actions",
                column: "capa_plan_id");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_findings_audit_id",
                table: "audit_findings",
                column: "audit_id");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_events_aggregate_id",
                table: "audit_events",
                column: "aggregate_id");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_events_aggregate_type",
                table: "audit_events",
                column: "aggregate_type");

            migrationBuilder.CreateIndex(
                name: "i_x_analytical_results_batch_id",
                table: "analytical_results",
                column: "batch_id");
        }
    }
}
