using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    full_name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    role = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    environment = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    facility = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    privilege = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    organization_id = table.Column<int>(type: "int", nullable: false),
                    organization_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    plant = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    two_factor_code_hash = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_users_organization_id",
                table: "users",
                column: "organization_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
