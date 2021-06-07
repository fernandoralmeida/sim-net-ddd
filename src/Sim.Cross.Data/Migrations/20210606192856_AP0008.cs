using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QSA");

            migrationBuilder.DropColumn(
                name: "Capital_Social",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Data_Situacao_Cadastral",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Data_Situacao_Especial",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Ente_Federativo_Resp",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Motivo_Situacao_Cadastral",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Natureza_Juridica",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Porte",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Situacao_Especial",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Empresa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Capital_Social",
                table: "Empresa",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Situacao_Cadastral",
                table: "Empresa",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data_Situacao_Especial",
                table: "Empresa",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ente_Federativo_Resp",
                table: "Empresa",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Motivo_Situacao_Cadastral",
                table: "Empresa",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Natureza_Juridica",
                table: "Empresa",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Porte",
                table: "Empresa",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Situacao_Especial",
                table: "Empresa",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Empresa",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QSA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    NomeRepLegal = table.Column<string>(type: "varchar(max)", nullable: true),
                    PaisOrigem = table.Column<string>(type: "varchar(max)", nullable: true),
                    Qual = table.Column<string>(type: "varchar(max)", nullable: true),
                    QualRepLegal = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QSA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QSA_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QSA_EmpresaId",
                table: "QSA",
                column: "EmpresaId");
        }
    }
}
