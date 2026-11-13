using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddManufacturingCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "master_formulas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    product_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    version = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    approved_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    approved_at = table.Column<DateOnly>(type: "date", nullable: true),
                    batch_size = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_master_formulas", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "production_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    product_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    master_formula_id = table.Column<int>(type: "int", nullable: false),
                    batch_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    planned_quantity = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    line = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    planned_start = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    planned_end = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_production_orders", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    dosage_form = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_products", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "raw_material_lots",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    receipt_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    material = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    category = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    supplier = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    supplier_lot = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    quantity = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    expiry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    storage_location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    certificate = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    packaging_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    identity_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sampling_status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    allocated_batch = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_raw_material_lots", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "formula_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    master_formula_id = table.Column<int>(type: "int", nullable: false),
                    kind = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    role = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    target = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    tolerance = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    control = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_formula_items", x => x.id);
                    table.ForeignKey(
                        name: "f_k_formula_items_master_formulas_master_formula_id",
                        column: x => x.master_formula_id,
                        principalTable: "master_formulas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    production_order_id = table.Column<int>(type: "int", nullable: false),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    equipment = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    progress = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    reference = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_operations", x => x.id);
                    table.ForeignKey(
                        name: "f_k_operations_production_orders_production_order_id",
                        column: x => x.production_order_id,
                        principalTable: "production_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_formula_items_master_formula_id",
                table: "formula_items",
                column: "master_formula_id");

            migrationBuilder.CreateIndex(
                name: "i_x_master_formulas_product_code_version",
                table: "master_formulas",
                columns: new[] { "product_code", "version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_operations_production_order_id_sequence",
                table: "operations",
                columns: new[] { "production_order_id", "sequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_production_orders_code",
                table: "production_orders",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_products_code",
                table: "products",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_raw_material_lots_code",
                table: "raw_material_lots",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "formula_items");

            migrationBuilder.DropTable(
                name: "operations");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "raw_material_lots");

            migrationBuilder.DropTable(
                name: "master_formulas");

            migrationBuilder.DropTable(
                name: "production_orders");
        }
    }
}
