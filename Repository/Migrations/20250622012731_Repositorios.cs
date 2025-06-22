using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BACKEND02.Migrations
{
    /// <inheritdoc />
    public partial class Repositorios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Marcas",
                newName: "NombreMarca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreMarca",
                table: "Marcas",
                newName: "Nombre");
        }
    }
}
