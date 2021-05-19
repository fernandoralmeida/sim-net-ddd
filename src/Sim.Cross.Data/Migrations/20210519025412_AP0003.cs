using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SebraeId",
                table: "Atendimento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RaeSebrae",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RAE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaeSebrae", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_SebraeId",
                table: "Atendimento",
                column: "SebraeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_RaeSebrae_SebraeId",
                table: "Atendimento",
                column: "SebraeId",
                principalTable: "RaeSebrae",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_RaeSebrae_SebraeId",
                table: "Atendimento");

            migrationBuilder.DropTable(
                name: "RaeSebrae");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_SebraeId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "SebraeId",
                table: "Atendimento");
        }
    }
}
