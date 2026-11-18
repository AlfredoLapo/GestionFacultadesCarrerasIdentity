using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionFacultadesCarrerasIdentity.Migrations
{
    /// <inheritdoc />
    public partial class RelacionFacultadCarrera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facultad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Decano = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultad", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "FacultadId",
                table: "Carrera",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carrera_FacultadId",
                table: "Carrera",
                column: "FacultadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrera_Facultad_FacultadId",
                table: "Carrera",
                column: "FacultadId",
                principalTable: "Facultad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrera_Facultad_FacultadId",
                table: "Carrera");

            migrationBuilder.DropIndex(
                name: "IX_Carrera_FacultadId",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "Carrera");

            migrationBuilder.DropTable(
                name: "Facultad");
        }
    }
}
