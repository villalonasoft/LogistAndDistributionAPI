using Microsoft.EntityFrameworkCore.Migrations;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "id", "backgroudcolor", "name" },
                values: new object[,]
                {
                    { 100, "fff2ed", "ENVIADO POR LA TIENDA" },
                    { 101, "ffeaca", "GESTIONADO EN EL CENTRO" },
                    { 102, "ffe89a", "INICIADO" },
                    { 103, "eaec62", "ASIGNADO" },
                    { 104, "b0f332", "PICKING" },
                    { 105, "00fa4c", "PICKING FINALIZADO" },
                    { 106, "00fa4c", "FACTURACION" },
                    { 107, "83c400", "DESPACHADO" },
                    { 108, "8f9100", "ENVIADO ELECTRONICAMENTE" },
                    { 109, "806300", "ENVIADO POR LA TIENDA" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 109);
        }
    }
}
