using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fim",
                table: "Atendimento");

            migrationBuilder.RenameColumn(
                name: "Inicio",
                table: "Atendimento",
                newName: "DataF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataF",
                table: "Atendimento",
                newName: "Inicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fim",
                table: "Atendimento",
                type: "datetime2",
                nullable: true);
        }
    }
}
