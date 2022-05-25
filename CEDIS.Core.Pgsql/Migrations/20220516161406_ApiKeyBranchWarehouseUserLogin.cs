using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class ApiKeyBranchWarehouseUserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "apikey",
                table: "warehouses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "orderdate",
                table: "orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apikey",
                table: "warehouses");

            migrationBuilder.DropColumn(
                name: "orderdate",
                table: "orders");
        }
    }
}
