using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteApp.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cuerpo",
                table: "Notes",
                newName: "Body");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Notes",
                newName: "Cuerpo");
        }
    }
}
