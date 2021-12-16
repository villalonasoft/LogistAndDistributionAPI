using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LogistAndDitribution.Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    InventoryFactor = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Units_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => new { x.Id, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => new { x.Id, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HallInit = table.Column<string>(nullable: true),
                    HallEnd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => new { x.Id, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_Zones_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    PersonTypeId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    LargeName = table.Column<string>(nullable: true),
                    CreditDay = table.Column<int>(nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(14, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => new { x.PersonTypeId, x.PersonId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_Customers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_PersonTypes_PersonTypeId",
                        column: x => x.PersonTypeId,
                        principalTable: "PersonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presentations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentations", x => new { x.Id, x.ProductId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_Presentations_Products_ProductId_CompanyId",
                        columns: x => new { x.ProductId, x.CompanyId },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    PersonTypeId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    OrderTypeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    InitDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Mount = table.Column<decimal>(type: "decimal(14, 2)", nullable: false),
                    Reference = table.Column<string>(maxLength: 25, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => new { x.Id, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_OrderHeaders_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_Customers_PersonTypeId_PersonId_CompanyId",
                        columns: x => new { x.PersonTypeId, x.PersonId, x.CompanyId },
                        principalTable: "Customers",
                        principalColumns: new[] { "PersonTypeId", "PersonId", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PresentationUnits",
                columns: table => new
                {
                    PresentationId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresentationUnits", x => new { x.UnitId, x.PresentationId, x.ProductId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_PresentationUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PresentationUnits_Presentations_PresentationId_ProductId_CompanyId",
                        columns: x => new { x.PresentationId, x.ProductId, x.CompanyId },
                        principalTable: "Presentations",
                        principalColumns: new[] { "Id", "ProductId", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderZoneUsers",
                columns: table => new
                {
                    OrderHeaderId = table.Column<int>(nullable: false),
                    ZoneId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    IsFinish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderZoneUsers", x => new { x.OrderHeaderId, x.ZoneId, x.UserId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_OrderZoneUsers_OrderHeaders_OrderHeaderId_CompanyId",
                        columns: x => new { x.OrderHeaderId, x.CompanyId },
                        principalTable: "OrderHeaders",
                        principalColumns: new[] { "Id", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderZoneUsers_Users_UserId_CompanyId",
                        columns: x => new { x.UserId, x.CompanyId },
                        principalTable: "Users",
                        principalColumns: new[] { "Id", "CompanyId" });
                    table.ForeignKey(
                        name: "FK_OrderZoneUsers_Zones_ZoneId_CompanyId",
                        columns: x => new { x.ZoneId, x.CompanyId },
                        principalTable: "Zones",
                        principalColumns: new[] { "Id", "CompanyId" });
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false),
                    PresentationId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    ZoneId = table.Column<int>(nullable: false),
                    Cant = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => new { x.ZoneId, x.UnitId, x.PresentationId, x.ProductId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_Stocks_Zones_ZoneId_CompanyId",
                        columns: x => new { x.ZoneId, x.CompanyId },
                        principalTable: "Zones",
                        principalColumns: new[] { "Id", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_PresentationUnits_UnitId_PresentationId_ProductId_CompanyId",
                        columns: x => new { x.UnitId, x.PresentationId, x.ProductId, x.CompanyId },
                        principalTable: "PresentationUnits",
                        principalColumns: new[] { "UnitId", "PresentationId", "ProductId", "CompanyId" });
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    ZoneId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    PresentationId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    OrderHeaderId = table.Column<int>(nullable: false),
                    CuantityOrder = table.Column<int>(nullable: false),
                    CuantityPicked = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.ZoneId, x.OrderHeaderId, x.UnitId, x.PresentationId, x.ProductId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_OrderDetail_OrderHeaders_OrderHeaderId_CompanyId",
                        columns: x => new { x.OrderHeaderId, x.CompanyId },
                        principalTable: "OrderHeaders",
                        principalColumns: new[] { "Id", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Stocks_ZoneId_UnitId_PresentationId_ProductId_CompanyId",
                        columns: x => new { x.ZoneId, x.UnitId, x.PresentationId, x.ProductId, x.CompanyId },
                        principalTable: "Stocks",
                        principalColumns: new[] { "ZoneId", "UnitId", "PresentationId", "ProductId", "CompanyId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyId",
                table: "Customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PersonId",
                table: "Customers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderHeaderId_CompanyId",
                table: "OrderDetail",
                columns: new[] { "OrderHeaderId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ZoneId_UnitId_PresentationId_ProductId_CompanyId",
                table: "OrderDetail",
                columns: new[] { "ZoneId", "UnitId", "PresentationId", "ProductId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_OrderTypeId",
                table: "OrderHeaders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders",
                columns: new[] { "PersonTypeId", "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_OrderHeaderId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "OrderHeaderId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_UserId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "UserId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderZoneUsers_ZoneId_CompanyId",
                table: "OrderZoneUsers",
                columns: new[] { "ZoneId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Document",
                table: "People",
                column: "Document",
                unique: true,
                filter: "[Document] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Presentations_ProductId_CompanyId",
                table: "Presentations",
                columns: new[] { "ProductId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_PresentationUnits_PresentationId_ProductId_CompanyId",
                table: "PresentationUnits",
                columns: new[] { "PresentationId", "ProductId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ZoneId_CompanyId",
                table: "Stocks",
                columns: new[] { "ZoneId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UnitId_PresentationId_ProductId_CompanyId",
                table: "Stocks",
                columns: new[] { "UnitId", "PresentationId", "ProductId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_ParentId",
                table: "Units",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_CompanyId",
                table: "Zones",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderZoneUsers");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "PresentationUnits");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Presentations");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "PersonTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
