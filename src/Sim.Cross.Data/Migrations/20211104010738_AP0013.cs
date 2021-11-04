using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Parceiros");

            migrationBuilder.AddColumn<Guid>(
                name: "SecretariaId",
                table: "Parceiros",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_SecretariaId",
                table: "Parceiros",
                column: "SecretariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_Secretaria_SecretariaId",
                table: "Parceiros",
                column: "SecretariaId",
                principalTable: "Secretaria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_Secretaria_SecretariaId",
                table: "Parceiros");

            migrationBuilder.DropIndex(
                name: "IX_Parceiros_SecretariaId",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "SecretariaId",
                table: "Parceiros");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Parceiros",
                type: "varchar(max)",
                nullable: true);
        }
    }
}
