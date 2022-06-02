using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class RemoveAtivoEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Evento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Evento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
