using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class SeedStatusDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "statusdetails",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "statusdetails",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 0, "PENDIENTE" },
                    { 1, "COMPLETO" },
                    { 2, "INCOMPLETO" },
                    { 3, "NO ENCONTRADO" },
                    { 4, "ERROR EXISTENCIA" },
                    { 5, "PENDIENTE A PROCESAR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "statusdetails",
                keyColumn: "id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "statusdetails",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "statusdetails",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "statusdetails",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "statusdetails",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "statusdetails",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "statusdetails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
