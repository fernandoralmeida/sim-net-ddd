using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AddSituacaoEventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Evento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Evento");
        }
    }
}
