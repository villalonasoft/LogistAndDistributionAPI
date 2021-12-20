using Microsoft.EntityFrameworkCore.Migrations;

namespace LogistAndDitribution.Core.Migrations
{
    public partial class IndexZoneUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderZoneUsers",
                table: "OrderZoneUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderZoneUsers_ZoneId_CompanyId",
                table: "OrderZoneUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderZoneUsers",
                table: "OrderZoneUsers",
                columns: new[] { "ZoneId", "OrderHeaderId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "UserId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_ZoneId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "ZoneId", "CompanyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderZoneUsers",
                table: "OrderZoneUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderZoneUsers_ZoneId_CompanyId",
                table: "OrderZoneUsers");

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_ZoneId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "ZoneId", "CompanyId" },
                unique: true);
        }
    }
}
