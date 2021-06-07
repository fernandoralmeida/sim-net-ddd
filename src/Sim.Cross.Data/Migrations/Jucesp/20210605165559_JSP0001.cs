using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations.Jucesp
{
    public partial class JSP0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseJucesp",
                columns: table => new
                {
                    Inscricao_Estadual = table.Column<string>(type: "varchar(50)", nullable: true),
                    CNPJ = table.Column<string>(type: "varchar(50)", nullable: true),
                    Nome_Empresarial = table.Column<string>(type: "varchar(255)", nullable: true),
                    Nome_Fantasia = table.Column<string>(type: "varchar(255)", nullable: true),
                    Natureza_Juridica = table.Column<string>(type: "varchar(255)", nullable: true),
                    Tipo_Logradouro = table.Column<string>(type: "varchar(50)", nullable: true),
                    Nome_Logradouro = table.Column<string>(type: "varchar(255)", nullable: true),
                    Numero_Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento_Logradouro = table.Column<string>(type: "varchar(50)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(255)", nullable: true),
                    Municipio = table.Column<string>(type: "varchar(50)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Situacao_Cadastral = table.Column<string>(type: "varchar(50)", nullable: true),
                    Data_Situacao_Cadastral = table.Column<string>(type: "varchar(10)", nullable: true),
                    Ocorrencia_Fiscal = table.Column<string>(type: "varchar(50)", nullable: true),
                    Regime_Apuracao = table.Column<string>(type: "varchar(50)", nullable: true),
                    Atividade_Economica = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseJucesp");
        }
    }
}
