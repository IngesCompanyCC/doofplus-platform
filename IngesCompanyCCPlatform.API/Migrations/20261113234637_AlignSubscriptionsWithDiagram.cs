using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IngesCompanyCCPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AlignSubscriptionsWithDiagram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "annual_price_currency",
                table: "plans");

            migrationBuilder.RenameColumn(
                name: "monthly_price_currency",
                table: "plans",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "monthly_price_amount",
                table: "plans",
                newName: "monthly_price");

            migrationBuilder.RenameColumn(
                name: "annual_price_amount",
                table: "plans",
                newName: "annual_price");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "subscriptions",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "card_brand",
                table: "subscriptions",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "billing_cycle",
                table: "subscriptions",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AddColumn<DateOnly>(
                name: "current_period_start",
                table: "subscriptions",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "plans",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "plans",
                type: "char(3)",
                fixedLength: true,
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldMaxLength: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_period_start",
                table: "subscriptions");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "plans",
                newName: "monthly_price_currency");

            migrationBuilder.RenameColumn(
                name: "monthly_price",
                table: "plans",
                newName: "monthly_price_amount");

            migrationBuilder.RenameColumn(
                name: "annual_price",
                table: "plans",
                newName: "annual_price_amount");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "subscriptions",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "card_brand",
                table: "subscriptions",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "billing_cycle",
                table: "subscriptions",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "plans",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "monthly_price_currency",
                table: "plans",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldFixedLength: true,
                oldMaxLength: 3);

            migrationBuilder.AddColumn<string>(
                name: "annual_price_currency",
                table: "plans",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }
    }
}
