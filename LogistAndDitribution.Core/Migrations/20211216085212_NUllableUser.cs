using Microsoft.EntityFrameworkCore.Migrations;

namespace LogistAndDitribution.Core.Migrations
{
    public partial class NUllableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderZoneUsers",
                table: "OrderZoneUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "OrderZoneUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderZoneUsers",
                table: "OrderZoneUsers",
                columns: new[] { "OrderHeaderId", "ZoneId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "UserId", "CompanyId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderZoneUsers",
                table: "OrderZoneUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "OrderZoneUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderZoneUsers",
                table: "OrderZoneUsers",
                columns: new[] { "OrderHeaderId", "ZoneId", "UserId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "UserId", "CompanyId" },
                unique: true);
        }
    }
}
