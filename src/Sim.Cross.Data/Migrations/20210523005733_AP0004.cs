using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Owner_AppUser_Id",
                table: "Inscricao",
                newName: "AplicationUser_Id");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Evento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Formato",
                table: "Evento",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "Formato",
                table: "Evento");

            migrationBuilder.RenameColumn(
                name: "AplicationUser_Id",
                table: "Inscricao",
                newName: "Owner_AppUser_Id");
        }
    }
}
