using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "branches",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    apikey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_branches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    abrebiature = table.Column<char>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nocoutns",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(maxLength: 10, nullable: true),
                    productname = table.Column<string>(nullable: true),
                    cost = table.Column<string>(nullable: true),
                    user = table.Column<string>(maxLength: 25, nullable: true),
                    cant = table.Column<int>(nullable: false),
                    stock = table.Column<int>(nullable: false),
                    refpedido = table.Column<int>(nullable: false),
                    countcant = table.Column<int>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nocoutns", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statusdetails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_statusdetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    backgroudcolor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "presentations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    productid = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_presentations", x => new { x.id, x.productid });
                    table.ForeignKey(
                        name: "fk_presentations_products_productid",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boxes",
                columns: table => new
                {
                    warehouseid = table.Column<int>(nullable: false),
                    id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_boxes", x => new { x.warehouseid, x.id });
                    table.ForeignKey(
                        name: "fk_boxes_warehouses_warehouseid",
                        column: x => x.warehouseid,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "branchorder",
                columns: table => new
                {
                    branchid = table.Column<int>(nullable: false),
                    orderid = table.Column<int>(nullable: false),
                    warehouseid = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: true),
                    statusid = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    mode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_branchorder", x => new { x.warehouseid, x.branchid, x.orderid });
                    table.ForeignKey(
                        name: "fk_branchorder_branches_branchid",
                        column: x => x.branchid,
                        principalTable: "branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_branchorder_statuses_statusid",
                        column: x => x.statusid,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_branchorder_warehouses_warehouseid",
                        column: x => x.warehouseid,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "zones",
                columns: table => new
                {
                    warehouseid = table.Column<int>(nullable: false),
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    initpasillo = table.Column<int>(nullable: false),
                    finpasillo = table.Column<int>(nullable: false),
                    inittramo = table.Column<int>(nullable: false),
                    fintramo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_zones", x => new { x.warehouseid, x.id });
                    table.ForeignKey(
                        name: "fk_zones_warehouses_warehouseid",
                        column: x => x.warehouseid,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    branchid = table.Column<int>(nullable: false),
                    orderid = table.Column<int>(nullable: false),
                    warehouseid = table.Column<int>(nullable: false),
                    zoneid = table.Column<int>(nullable: false),
                    dateinit = table.Column<DateTime>(nullable: false),
                    dateend = table.Column<DateTime>(nullable: false),
                    userid = table.Column<int>(nullable: true),
                    statusid = table.Column<int>(nullable: false),
                    modeid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => new { x.warehouseid, x.branchid, x.orderid, x.zoneid });
                    table.ForeignKey(
                        name: "fk_orders_branches_branchid",
                        column: x => x.branchid,
                        principalTable: "branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_modes_modeid",
                        column: x => x.modeid,
                        principalTable: "modes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_statuses_statusid",
                        column: x => x.statusid,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orders_zones_warehouseid_zoneid",
                        columns: x => new { x.warehouseid, x.zoneid },
                        principalTable: "zones",
                        principalColumns: new[] { "warehouseid", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_branchorder_warehouseid_branchid_orderid",
                        columns: x => new { x.warehouseid, x.branchid, x.orderid },
                        principalTable: "branchorder",
                        principalColumns: new[] { "warehouseid", "branchid", "orderid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "presentationwarehouses",
                columns: table => new
                {
                    warehouseid = table.Column<int>(nullable: false),
                    presentationid = table.Column<int>(nullable: false),
                    productid = table.Column<int>(nullable: false),
                    pasillo = table.Column<int>(nullable: false),
                    tramo = table.Column<int>(nullable: false),
                    bandeja = table.Column<char>(nullable: false),
                    ubitramo = table.Column<int>(nullable: false),
                    zoneid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_presentationwarehouses", x => new { x.warehouseid, x.presentationid, x.productid });
                    table.ForeignKey(
                        name: "fk_presentationwarehouses_warehouses_warehouseid",
                        column: x => x.warehouseid,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_presentationwarehouses_presentations_presentationid_product~",
                        columns: x => new { x.presentationid, x.productid },
                        principalTable: "presentations",
                        principalColumns: new[] { "id", "productid" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_presentationwarehouses_zones_warehouseid_zoneid",
                        columns: x => new { x.warehouseid, x.zoneid },
                        principalTable: "zones",
                        principalColumns: new[] { "warehouseid", "id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "branchorderdetail",
                columns: table => new
                {
                    warehouseid = table.Column<int>(nullable: false),
                    branchid = table.Column<int>(nullable: false),
                    orderid = table.Column<int>(nullable: false),
                    productid = table.Column<int>(nullable: false),
                    presentationid = table.Column<int>(nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    factor = table.Column<int>(nullable: false),
                    unitid = table.Column<int>(nullable: false),
                    orderedquantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_branchorderdetail", x => new { x.warehouseid, x.branchid, x.orderid, x.presentationid, x.productid });
                    table.ForeignKey(
                        name: "fk_branchorderdetail_units_unitid",
                        column: x => x.unitid,
                        principalTable: "units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_branchorderdetail_branchorder_warehouseid_branchid_orderid",
                        columns: x => new { x.warehouseid, x.branchid, x.orderid },
                        principalTable: "branchorder",
                        principalColumns: new[] { "warehouseid", "branchid", "orderid" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_branchorderdetail_presentationwarehouses_warehouseid_presen~",
                        columns: x => new { x.warehouseid, x.presentationid, x.productid },
                        principalTable: "presentationwarehouses",
                        principalColumns: new[] { "warehouseid", "presentationid", "productid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    branchid = table.Column<int>(nullable: false),
                    orderid = table.Column<int>(nullable: false),
                    zoneid = table.Column<int>(nullable: false),
                    warehouseid = table.Column<int>(nullable: false),
                    presentationid = table.Column<int>(nullable: false),
                    productid = table.Column<int>(nullable: false),
                    statusid = table.Column<int>(nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    unitid = table.Column<int>(nullable: false),
                    quantityavailable = table.Column<int>(nullable: false),
                    countedquantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_details", x => new { x.warehouseid, x.branchid, x.orderid, x.zoneid, x.presentationid, x.productid });
                    table.ForeignKey(
                        name: "fk_details_statusdetails_statusid",
                        column: x => x.statusid,
                        principalTable: "statusdetails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_details_units_unitid",
                        column: x => x.unitid,
                        principalTable: "units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_details_presentationwarehouses_warehouseid_presentationid_p~",
                        columns: x => new { x.warehouseid, x.presentationid, x.productid },
                        principalTable: "presentationwarehouses",
                        principalColumns: new[] { "warehouseid", "presentationid", "productid" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_details_orders_warehouseid_branchid_orderid_zoneid",
                        columns: x => new { x.warehouseid, x.branchid, x.orderid, x.zoneid },
                        principalTable: "orders",
                        principalColumns: new[] { "warehouseid", "branchid", "orderid", "zoneid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_branchorder_statusid",
                table: "branchorder",
                column: "statusid");

            migrationBuilder.CreateIndex(
                name: "ix_branchorder_branchid_reference",
                table: "branchorder",
                columns: new[] { "branchid", "reference" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_branchorderdetail_unitid",
                table: "branchorderdetail",
                column: "unitid");

            migrationBuilder.CreateIndex(
                name: "ix_branchorderdetail_warehouseid_presentationid_productid",
                table: "branchorderdetail",
                columns: new[] { "warehouseid", "presentationid", "productid" });

            migrationBuilder.CreateIndex(
                name: "ix_details_statusid",
                table: "details",
                column: "statusid");

            migrationBuilder.CreateIndex(
                name: "ix_details_unitid",
                table: "details",
                column: "unitid");

            migrationBuilder.CreateIndex(
                name: "ix_details_warehouseid_presentationid_productid",
                table: "details",
                columns: new[] { "warehouseid", "presentationid", "productid" });

            migrationBuilder.CreateIndex(
                name: "ix_orders_branchid",
                table: "orders",
                column: "branchid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_modeid",
                table: "orders",
                column: "modeid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_statusid",
                table: "orders",
                column: "statusid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_userid",
                table: "orders",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_orders_warehouseid_zoneid",
                table: "orders",
                columns: new[] { "warehouseid", "zoneid" });

            migrationBuilder.CreateIndex(
                name: "ix_presentations_productid",
                table: "presentations",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "ix_presentationwarehouses_presentationid_productid",
                table: "presentationwarehouses",
                columns: new[] { "presentationid", "productid" });

            migrationBuilder.CreateIndex(
                name: "ix_presentationwarehouses_warehouseid_zoneid",
                table: "presentationwarehouses",
                columns: new[] { "warehouseid", "zoneid" });

            migrationBuilder.Sql($@"
CREATE OR REPLACE FUNCTION changeZone()
  RETURNS TRIGGER 
  LANGUAGE PLPGSQL
  AS
$$
declare varzoneId int := 0;
BEGIN
select b.Id into STRICT varzoneId
From public.presentationwarehouses a 
INNER JOIN public.Zones b 
ON (a.Pasillo between b.InitPasillo and b.FinPasillo) AND ((a.Pasillo >= InitPasillo AND Tramo >= InitTramo) OR (a.Pasillo <= FinPasillo AND Tramo <= FinTramo))
where a.productid=new.productid and a.presentationid=new.presentationid and a.warehouseid = new.warehouseid and b.warehouseid = new.warehouseid;

update public.presentationwarehouses set zoneid = varzoneId where productid = new.productid and presentationid = new.presentationid and warehouseid=new.warehouseid;
	RETURN NEW;
END;
$$
");

            migrationBuilder.Sql($@"
CREATE or replace TRIGGER last_zone_changes
  after update of pasillo,tramo,bandeja,ubitramo
  ON public.presentationwarehouses
  FOR EACH ROW
  EXECUTE PROCEDURE changeZone();
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "boxes");

            migrationBuilder.DropTable(
                name: "branchorderdetail");

            migrationBuilder.DropTable(
                name: "details");

            migrationBuilder.DropTable(
                name: "nocoutns");

            migrationBuilder.DropTable(
                name: "statusdetails");

            migrationBuilder.DropTable(
                name: "units");

            migrationBuilder.DropTable(
                name: "presentationwarehouses");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "presentations");

            migrationBuilder.DropTable(
                name: "modes");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "zones");

            migrationBuilder.DropTable(
                name: "branchorder");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "branches");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "warehouses");
        }
    }
}
