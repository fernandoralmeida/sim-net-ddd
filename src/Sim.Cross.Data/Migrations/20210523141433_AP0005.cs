using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner_Setor",
                table: "Inscricao");

            migrationBuilder.AddColumn<bool>(
                name: "Presente",
                table: "Inscricao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Presente",
                table: "Inscricao");

            migrationBuilder.AddColumn<string>(
                name: "Owner_Setor",
                table: "Inscricao",
                type: "varchar(max)",
                nullable: true);
        }
    }
}
