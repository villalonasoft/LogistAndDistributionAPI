using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class BoxOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "boxorders",
                columns: table => new
                {
                    warehouseid = table.Column<int>(nullable: false),
                    branchid = table.Column<int>(nullable: false),
                    orderid = table.Column<int>(nullable: false),
                    zoneid = table.Column<int>(nullable: false),
                    boxid = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    statusid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_boxorders", x => new { x.warehouseid, x.branchid, x.orderid, x.zoneid, x.boxid });
                    table.ForeignKey(
                        name: "fk_boxorders_boxes_warehouseid_boxid",
                        columns: x => new { x.warehouseid, x.boxid },
                        principalTable: "boxes",
                        principalColumns: new[] { "warehouseid", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_boxorders_orders_warehouseid_branchid_orderid_zoneid",
                        columns: x => new { x.warehouseid, x.branchid, x.orderid, x.zoneid },
                        principalTable: "orders",
                        principalColumns: new[] { "warehouseid", "branchid", "orderid", "zoneid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_boxorders_warehouseid_boxid",
                table: "boxorders",
                columns: new[] { "warehouseid", "boxid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "boxorders");
        }
    }
}
