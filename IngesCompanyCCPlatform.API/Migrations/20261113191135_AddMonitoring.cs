using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMonitoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    sensor_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    equipment_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    severity = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    status = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    detail = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    batch_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    acknowledged_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    acknowledged_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    response_note = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    linked_record = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "equipment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    model = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    calibration_status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    calibration_due = table.Column<DateOnly>(type: "date", nullable: true),
                    certificate = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    next_maintenance = table.Column<DateOnly>(type: "date", nullable: true),
                    maintenance_owner = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    work_order = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    health_note = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_equipment", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "readings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sensor_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    batch_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    value = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    recorded_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    source = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_readings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sensors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    device_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    unit = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    equipment_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    lower_limit = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    upper_limit = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    assigned_batch = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_sensors", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_alerts_batch_code",
                table: "alerts",
                column: "batch_code");

            migrationBuilder.CreateIndex(
                name: "i_x_alerts_code",
                table: "alerts",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_alerts_sensor_code",
                table: "alerts",
                column: "sensor_code");

            migrationBuilder.CreateIndex(
                name: "i_x_equipment_code",
                table: "equipment",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_readings_batch_code",
                table: "readings",
                column: "batch_code");

            migrationBuilder.CreateIndex(
                name: "i_x_readings_sensor_code_recorded_at",
                table: "readings",
                columns: new[] { "sensor_code", "recorded_at" });

            migrationBuilder.CreateIndex(
                name: "i_x_sensors_code",
                table: "sensors",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_sensors_device_id",
                table: "sensors",
                column: "device_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_sensors_equipment_code",
                table: "sensors",
                column: "equipment_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alerts");

            migrationBuilder.DropTable(
                name: "equipment");

            migrationBuilder.DropTable(
                name: "readings");

            migrationBuilder.DropTable(
                name: "sensors");
        }
    }
}
