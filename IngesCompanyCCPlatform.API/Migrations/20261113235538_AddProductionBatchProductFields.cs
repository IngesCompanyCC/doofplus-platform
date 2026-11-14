using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionBatchProductFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "line",
                table: "production_batches",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "production_batches",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "progress",
                table: "production_batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "tone",
                table: "batch_events",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Normal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "line",
                table: "production_batches");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "production_batches");

            migrationBuilder.DropColumn(
                name: "progress",
                table: "production_batches");

            migrationBuilder.DropColumn(
                name: "tone",
                table: "batch_events");
        }
    }
}
