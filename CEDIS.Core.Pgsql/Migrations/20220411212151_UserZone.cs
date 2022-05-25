using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class UserZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userzones",
                columns: table => new
                {
                    userid = table.Column<int>(nullable: false),
                    id = table.Column<int>(nullable: false),
                    warehouseid = table.Column<int>(nullable: false),
                    zoneid = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    isactive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_userzones", x => new { x.userid, x.id });
                    table.ForeignKey(
                        name: "fk_userzones_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_userzones_zones_warehouseid_zoneid",
                        columns: x => new { x.warehouseid, x.zoneid },
                        principalTable: "zones",
                        principalColumns: new[] { "warehouseid", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_userzones_warehouseid_zoneid",
                table: "userzones",
                columns: new[] { "warehouseid", "zoneid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userzones");
        }
    }
}
