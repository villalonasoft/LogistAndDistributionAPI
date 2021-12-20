using Microsoft.EntityFrameworkCore.Migrations;

namespace LogistAndDitribution.Core.Migrations
{
    public partial class CustomerIdRelationSheep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Customers_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderHeaders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                columns: new[] { "Id", "PersonTypeId", "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_CustomerId_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders",
                columns: new[] { "CustomerId", "PersonTypeId", "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PersonTypeId",
                table: "Customers",
                column: "PersonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Customers_CustomerId_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders",
                columns: new[] { "CustomerId", "PersonTypeId", "PersonId", "CompanyId" },
                principalTable: "Customers",
                principalColumns: new[] { "Id", "PersonTypeId", "PersonId", "CompanyId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Customers_CustomerId_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_CustomerId_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PersonTypeId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderHeaders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                columns: new[] { "PersonTypeId", "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders",
                columns: new[] { "PersonTypeId", "PersonId", "CompanyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Customers_PersonTypeId_PersonId_CompanyId",
                table: "OrderHeaders",
                columns: new[] { "PersonTypeId", "PersonId", "CompanyId" },
                principalTable: "Customers",
                principalColumns: new[] { "PersonTypeId", "PersonId", "CompanyId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
