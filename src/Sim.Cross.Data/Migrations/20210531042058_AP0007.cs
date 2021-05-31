using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Pessoa",
                type: "varchar(max)",
                nullable: false,
                collation: "Latin1_General_CI_AI",
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Pessoa",
                type: "varchar(max)",
                nullable: true,
                collation: "Latin1_General_CI_AI",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Pessoa",
                type: "varchar(max)",
                nullable: true,
                collation: "Latin1_General_CI_AI",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Pessoa",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldCollation: "Latin1_General_CI_AI");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Pessoa",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true,
                oldCollation: "Latin1_General_CI_AI");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Pessoa",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true,
                oldCollation: "Latin1_General_CI_AI");
        }
    }
}
