using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddQuality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "analytical_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    batch_id = table.Column<int>(type: "int", nullable: false),
                    test_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    parameter = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    measured_value = table.Column<decimal>(type: "decimal(14,4)", precision: 14, scale: 4, nullable: false),
                    minimum_value = table.Column<decimal>(type: "decimal(14,4)", precision: 14, scale: 4, nullable: false),
                    maximum_value = table.Column<decimal>(type: "decimal(14,4)", precision: 14, scale: 4, nullable: false),
                    unit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    reviewed_by = table.Column<int>(type: "int", nullable: true),
                    tested_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    reviewed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_analytical_results", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "audit_events",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    aggregate_type = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    aggregate_id = table.Column<int>(type: "int", nullable: false),
                    event_type = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    performed_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    occurred_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_audit_events", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "audit_findings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    audit_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    severity = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    related_deviation_id = table.Column<int>(type: "int", nullable: true),
                    identified_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_audit_findings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "audits",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    scope = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    audit_type = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    auditor_id = table.Column<int>(type: "int", nullable: true),
                    planned_date = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    started_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    completed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_audits", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "batch_releases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    batch_id = table.Column<int>(type: "int", nullable: false),
                    batch_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    requested_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    decision = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    signature_reference = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    signed_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    signed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_batch_releases", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "capa_actions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    capa_plan_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    assigned_to = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    due_date = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_capa_actions", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "capa_plans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    deviation_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    due_date = table.Column<DateTime>(type: "date", nullable: false),
                    responsible = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_capa_plans", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "collaboration_tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    assigned_to = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    related_deviation_id = table.Column<int>(type: "int", nullable: true),
                    due_date = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_collaboration_tasks", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "deviations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    severity = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    batch_id = table.Column<int>(type: "int", nullable: false),
                    assigned_to = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    detected_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    closed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_deviations", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evidences",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    type = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    file_reference = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    deviation_id = table.Column<int>(type: "int", nullable: true),
                    document_id = table.Column<int>(type: "int", nullable: true),
                    uploaded_by = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    uploaded_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    verified_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_evidences", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "quality_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    document_type = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    file_reference = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    version = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    owner_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    approved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_quality_documents", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "regulatory_reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    regulatory_authority = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    file_reference = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    due_date = table.Column<DateTime>(type: "date", nullable: true),
                    prepared_by = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    submitted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_regulatory_reports", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "task_comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    task_id = table.Column<int>(type: "int", nullable: false),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_task_comments", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "traceability_gaps",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    batch_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    missing_reference = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    affected_process = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    reviewed_by = table.Column<int>(type: "int", nullable: true),
                    detected_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    resolved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_traceability_gaps", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_analytical_results_batch_id",
                table: "analytical_results",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_events_aggregate_id",
                table: "audit_events",
                column: "aggregate_id");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_events_aggregate_type",
                table: "audit_events",
                column: "aggregate_type");

            migrationBuilder.CreateIndex(
                name: "i_x_audit_findings_audit_id",
                table: "audit_findings",
                column: "audit_id");

            migrationBuilder.CreateIndex(
                name: "i_x_audits_code",
                table: "audits",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_batch_releases_batch_id",
                table: "batch_releases",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "i_x_capa_actions_capa_plan_id",
                table: "capa_actions",
                column: "capa_plan_id");

            migrationBuilder.CreateIndex(
                name: "i_x_deviations_batch_id",
                table: "deviations",
                column: "batch_id");

            migrationBuilder.CreateIndex(
                name: "i_x_deviations_code",
                table: "deviations",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_evidences_deviation_id",
                table: "evidences",
                column: "deviation_id");

            migrationBuilder.CreateIndex(
                name: "i_x_quality_documents_code",
                table: "quality_documents",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_regulatory_reports_code",
                table: "regulatory_reports",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_task_comments_task_id",
                table: "task_comments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "i_x_traceability_gaps_batch_id",
                table: "traceability_gaps",
                column: "batch_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analytical_results");

            migrationBuilder.DropTable(
                name: "audit_events");

            migrationBuilder.DropTable(
                name: "audit_findings");

            migrationBuilder.DropTable(
                name: "audits");

            migrationBuilder.DropTable(
                name: "batch_releases");

            migrationBuilder.DropTable(
                name: "capa_actions");

            migrationBuilder.DropTable(
                name: "capa_plans");

            migrationBuilder.DropTable(
                name: "collaboration_tasks");

            migrationBuilder.DropTable(
                name: "deviations");

            migrationBuilder.DropTable(
                name: "evidences");

            migrationBuilder.DropTable(
                name: "quality_documents");

            migrationBuilder.DropTable(
                name: "regulatory_reports");

            migrationBuilder.DropTable(
                name: "task_comments");

            migrationBuilder.DropTable(
                name: "traceability_gaps");
        }
    }
}
