using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class Qr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "twofactorautenticationid",
                table: "users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "twofactorauthenticator",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    generator = table.Column<byte[]>(nullable: true),
                    secretkey = table.Column<string>(nullable: true),
                    secret = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_twofactorauthenticator", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_twofactorautenticationid",
                table: "users",
                column: "twofactorautenticationid");

            migrationBuilder.AddForeignKey(
                name: "fk_users_twofactorauthenticator_twofactorautenticationid",
                table: "users",
                column: "twofactorautenticationid",
                principalTable: "twofactorauthenticator",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_twofactorauthenticator_twofactorautenticationid",
                table: "users");

            migrationBuilder.DropTable(
                name: "twofactorauthenticator");

            migrationBuilder.DropIndex(
                name: "ix_users_twofactorautenticationid",
                table: "users");

            migrationBuilder.DropColumn(
                name: "twofactorautenticationid",
                table: "users");
        }
    }
}
