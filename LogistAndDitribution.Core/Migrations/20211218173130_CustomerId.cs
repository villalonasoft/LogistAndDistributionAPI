using Microsoft.EntityFrameworkCore.Migrations;

namespace LogistAndDitribution.Core.Migrations
{
    public partial class CustomerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "OrderHeaders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Customers");
        }
    }
}
