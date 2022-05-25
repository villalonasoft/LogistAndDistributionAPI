using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class ConcurrencyTokenOrderDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rowversion",
                table: "orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "rowversion",
                table: "orders",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }
    }
}
