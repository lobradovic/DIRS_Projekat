using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DIRS_Ketering.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jela",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    Cena = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jela", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ime = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Lozinka = table.Column<string>(type: "TEXT", nullable: false),
                    Rola = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VremeKreiranja = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    KorisnikId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stavke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kolicina = table.Column<int>(type: "INTEGER", nullable: false),
                    TrenutnaCena = table.Column<decimal>(type: "TEXT", nullable: false),
                    PorudzbineId = table.Column<int>(type: "INTEGER", nullable: false),
                    JeloId = table.Column<int>(type: "INTEGER", nullable: false),
                    PorudzbinaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stavke_Jela_JeloId",
                        column: x => x.JeloId,
                        principalTable: "Jela",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stavke_Porudzbine_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jela",
                columns: new[] { "Id", "Cena", "Naziv", "Opis" },
                values: new object[,]
                {
                    { 1, 5500m, "Plata suhomesnatih proizvoda", "Idealno predjelo" },
                    { 2, 200m, "Brusketi sa lososom", "Finger food za sve vrste proslava" },
                    { 3, 1500m, "Cezar salata", "" },
                    { 4, 1200m, "Pileci stapici", "Hrskavi pileci stapici" }
                });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "Id", "Email", "Ime", "Lozinka", "Rola" },
                values: new object[] { 1, "admin@admin.com", "Admin", "admin123", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_KorisnikId",
                table: "Porudzbine",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_JeloId",
                table: "Stavke",
                column: "JeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_PorudzbinaId",
                table: "Stavke",
                column: "PorudzbinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stavke");

            migrationBuilder.DropTable(
                name: "Jela");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
