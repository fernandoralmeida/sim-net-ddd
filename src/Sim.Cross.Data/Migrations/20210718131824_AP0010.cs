using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ultima_Alteracao",
                table: "Planer",
                newName: "DataInicial");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Planer",
                newName: "DataFinal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInicial",
                table: "Planer",
                newName: "Ultima_Alteracao");

            migrationBuilder.RenameColumn(
                name: "DataFinal",
                table: "Planer",
                newName: "Data");
        }
    }
}
