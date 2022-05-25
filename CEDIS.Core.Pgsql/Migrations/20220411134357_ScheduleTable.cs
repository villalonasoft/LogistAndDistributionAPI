using Microsoft.EntityFrameworkCore.Migrations;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class ScheduleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    sunday = table.Column<bool>(nullable: false),
                    monday = table.Column<bool>(nullable: false),
                    tuesday = table.Column<bool>(nullable: false),
                    wednesday = table.Column<bool>(nullable: false),
                    thursday = table.Column<bool>(nullable: false),
                    friday = table.Column<bool>(nullable: false),
                    saturday = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_schedules_branches_id",
                        column: x => x.id,
                        principalTable: "branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedules");
        }
    }
}
