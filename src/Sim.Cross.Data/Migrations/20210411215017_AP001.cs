using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    Owner = table.Column<string>(type: "varchar(50)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DIA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscricaoMunicipal = table.Column<int>(type: "int", nullable: false),
                    Autorizacao = table.Column<string>(type: "varchar(256)", nullable: false),
                    Titular = table.Column<string>(type: "varchar(150)", nullable: true),
                    Auxiliar = table.Column<string>(type: "varchar(150)", nullable: true),
                    Atividade = table.Column<string>(type: "varchar(256)", nullable: true),
                    FormaAtuacao = table.Column<string>(type: "varchar(150)", nullable: true),
                    Veiculo = table.Column<string>(type: "varchar(50)", nullable: true),
                    Emissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Processo = table.Column<string>(type: "varchar(15)", nullable: true),
                    Situacao = table.Column<string>(type: "varchar(20)", nullable: true),
                    DiaDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "varchar(18)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(20)", nullable: true),
                    Data_Abertura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome_Empresarial = table.Column<string>(type: "varchar(150)", nullable: true),
                    Nome_Fantasia = table.Column<string>(type: "varchar(150)", nullable: true),
                    Porte = table.Column<string>(type: "varchar(20)", nullable: true),
                    CNAE_Principal = table.Column<string>(type: "varchar(20)", nullable: true),
                    Atividade_Principal = table.Column<string>(type: "varchar(999)", nullable: true),
                    Atividade_Secundarias = table.Column<string>(type: "varchar(999)", nullable: true),
                    Natureza_Juridica = table.Column<string>(type: "varchar(150)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(150)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(5)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(20)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: true),
                    Municipio = table.Column<string>(type: "varchar(50)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: true),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Ente_Federativo_Resp = table.Column<string>(type: "varchar(50)", nullable: true),
                    Situacao_Cadastral = table.Column<string>(type: "varchar(50)", nullable: true),
                    Data_Situacao_Cadastral = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Motivo_Situacao_Cadastral = table.Column<string>(type: "varchar(50)", nullable: true),
                    Situacao_Especial = table.Column<string>(type: "varchar(50)", nullable: true),
                    Data_Situacao_Especial = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Capital_Social = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Inscritos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inscricao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evento_Id = table.Column<int>(type: "int", nullable: false),
                    Pessoa_Id = table.Column<int>(type: "int", nullable: false),
                    Empresa_Id = table.Column<int>(type: "int", nullable: false),
                    Data_Inscricao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Nome_Social = table.Column<string>(type: "varchar(150)", nullable: true),
                    Data_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "varchar(14)", nullable: false),
                    RG = table.Column<string>(type: "varchar(12)", nullable: true),
                    RG_Emissor = table.Column<string>(type: "varchar(4)", nullable: true),
                    RG_Emissor_UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deficiencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(150)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(5)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(20)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(50)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Tel_Movel = table.Column<string>(type: "varchar(15)", nullable: true),
                    Tel_Fixo = table.Column<string>(type: "varchar(15)", nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: true),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Segunda = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Terca = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Quarta = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Quinta = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Sexta = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Sabado = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ProximaSemana = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Prioridades = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Anotacao = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Secretaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    Owner = table.Column<string>(type: "varchar(50)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    Owner = table.Column<string>(type: "varchar(50)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    Owner = table.Column<string>(type: "varchar(50)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QSA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: true),
                    Qualificacao = table.Column<string>(type: "varchar(50)", nullable: true),
                    Empresa_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QSA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QSA_Empresa_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ambulante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Protocolo = table.Column<string>(type: "varchar(256)", nullable: false),
                    Titular = table.Column<string>(type: "varchar(256)", nullable: true),
                    Auxiliar = table.Column<string>(type: "varchar(256)", nullable: true),
                    FormaAtuacao = table.Column<string>(type: "varchar(50)", nullable: true),
                    Local = table.Column<string>(type: "varchar(50)", nullable: true),
                    Atividade = table.Column<string>(type: "varchar(256)", nullable: true),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Contador = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambulante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ambulante_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Protocolo = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pessoa_Id = table.Column<int>(type: "int", nullable: false),
                    Empresa_Id = table.Column<int>(type: "int", nullable: false),
                    Setor = table.Column<string>(type: "varchar(50)", nullable: true),
                    Canal = table.Column<string>(type: "varchar(50)", nullable: true),
                    Servicos = table.Column<string>(type: "varchar(150)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimento_Empresa_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimento_Pessoa_Pessoa_Id",
                        column: x => x.Pessoa_Id,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ambulante_PessoaId",
                table: "Ambulante",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambulante_Protocolo",
                table: "Ambulante",
                column: "Protocolo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_Empresa_Id",
                table: "Atendimento",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_Pessoa_Id",
                table: "Atendimento",
                column: "Pessoa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_Protocolo",
                table: "Atendimento",
                column: "Protocolo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DIA_Autorizacao",
                table: "DIA",
                column: "Autorizacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_CNPJ",
                table: "Empresa",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CPF",
                table: "Pessoa",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QSA_Empresa_Id",
                table: "QSA",
                column: "Empresa_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ambulante");

            migrationBuilder.DropTable(
                name: "Atendimento");

            migrationBuilder.DropTable(
                name: "Canal");

            migrationBuilder.DropTable(
                name: "DIA");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Inscricao");

            migrationBuilder.DropTable(
                name: "Planer");

            migrationBuilder.DropTable(
                name: "QSA");

            migrationBuilder.DropTable(
                name: "Secretaria");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
