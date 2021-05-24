using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Evento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Evento",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
