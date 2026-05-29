using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DIRS_Ketering.Migrations
{
    /// <inheritdoc />
    public partial class updatePorudzbinaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdresaIsporuke",
                table: "Porudzbine",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumIsporuke",
                table: "Porudzbine",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdresaIsporuke",
                table: "Porudzbine");

            migrationBuilder.DropColumn(
                name: "DatumIsporuke",
                table: "Porudzbine");
        }
    }
}
