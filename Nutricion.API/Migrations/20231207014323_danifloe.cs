using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutricion.API.Migrations
{
    /// <inheritdoc />
    public partial class danifloe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user",
                table: "registroSongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user",
                table: "registroSongs");
        }
    }
}
