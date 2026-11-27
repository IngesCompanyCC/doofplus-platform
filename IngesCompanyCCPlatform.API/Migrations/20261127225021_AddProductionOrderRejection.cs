using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionOrderRejection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rejection_reason",
                table: "production_orders",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rejection_reason",
                table: "production_orders");
        }
    }
}
