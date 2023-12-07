using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutricion.API.Migrations
{
    /// <inheritdoc />
    public partial class test7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosIMC");

            migrationBuilder.CreateTable(
                name: "registroSongs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    songName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    albumCoverLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    artistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    songDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    displayDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    displayArtist = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registroSongs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registroSongs");

            migrationBuilder.CreateTable(
                name: "RegistrosIMC",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Altura = table.Column<float>(type: "real", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Resultado = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosIMC", x => x.ID);
                });
        }
    }
}
