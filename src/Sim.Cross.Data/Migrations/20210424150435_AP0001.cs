using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sim.Cross.Data.Migrations
{
    public partial class AP0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Data_Situacao_Especial = table.Column<string>(type: "varchar(10)", nullable: true),
                    Capital_Social = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lotacao = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Segunda = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Terca = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Quarta = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Quinta = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Sexta = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Sabado = table.Column<string>(type: "varchar(2000)", nullable: true),
                    ProximaSemana = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Prioridades = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Anotacao = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Owner_AppUser_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QSA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: true),
                    Qualificacao = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QSA", x => x.Id);
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
                name: "Ambulante",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Protocolo = table.Column<string>(type: "varchar(256)", nullable: false),
                    FormaAtuacao = table.Column<string>(type: "varchar(50)", nullable: true),
                    Local = table.Column<string>(type: "varchar(50)", nullable: true),
                    Atividade = table.Column<string>(type: "varchar(256)", nullable: true),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    TitularId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuxiliarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambulante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ambulante_Pessoa_AuxiliarId",
                        column: x => x.AuxiliarId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ambulante_Pessoa_TitularId",
                        column: x => x.TitularId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Protocolo = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Setor = table.Column<string>(type: "varchar(50)", nullable: true),
                    Canal = table.Column<string>(type: "varchar(50)", nullable: true),
                    Servicos = table.Column<string>(type: "varchar(150)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Owner_AppUser_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimento_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inscricao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Owner_Setor = table.Column<string>(type: "varchar(20)", nullable: true),
                    Owner_AppUser_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data_Inscricao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParticipanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscricao_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscricao_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscricao_Pessoa_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaQSA",
                columns: table => new
                {
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QSAsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaQSA", x => new { x.EmpresaId, x.QSAsId });
                    table.ForeignKey(
                        name: "FK_EmpresaQSA_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaQSA_QSA_QSAsId",
                        column: x => x.QSAsId,
                        principalTable: "QSA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    SecretariaId = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DIA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InscricaoMunicipal = table.Column<int>(type: "int", nullable: false),
                    Autorizacao = table.Column<string>(type: "varchar(256)", nullable: false),
                    Atividade = table.Column<string>(type: "varchar(256)", nullable: true),
                    FormaAtuacao = table.Column<string>(type: "varchar(150)", nullable: true),
                    Veiculo = table.Column<string>(type: "varchar(50)", nullable: true),
                    Emissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Processo = table.Column<string>(type: "varchar(15)", nullable: true),
                    Situacao = table.Column<string>(type: "varchar(20)", nullable: true),
                    DiaDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TitularId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuxiliarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmbulanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DIA_Ambulante_AmbulanteId",
                        column: x => x.AmbulanteId,
                        principalTable: "Ambulante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DIA_Pessoa_AuxiliarId",
                        column: x => x.AuxiliarId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DIA_Pessoa_TitularId",
                        column: x => x.TitularId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Canal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    SecretariaId = table.Column<int>(type: "int", nullable: true),
                    SetorId = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canal_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Canal_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    SecretariaId = table.Column<int>(type: "int", nullable: true),
                    SetorId = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servico_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servico_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ambulante_AuxiliarId",
                table: "Ambulante",
                column: "AuxiliarId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambulante_Protocolo",
                table: "Ambulante",
                column: "Protocolo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ambulante_TitularId",
                table: "Ambulante",
                column: "TitularId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_EmpresaId",
                table: "Atendimento",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_PessoaId",
                table: "Atendimento",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_Protocolo",
                table: "Atendimento",
                column: "Protocolo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Canal_SecretariaId",
                table: "Canal",
                column: "SecretariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Canal_SetorId",
                table: "Canal",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_DIA_AmbulanteId",
                table: "DIA",
                column: "AmbulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_DIA_Autorizacao",
                table: "DIA",
                column: "Autorizacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DIA_AuxiliarId",
                table: "DIA",
                column: "AuxiliarId");

            migrationBuilder.CreateIndex(
                name: "IX_DIA_TitularId",
                table: "DIA",
                column: "TitularId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_CNPJ",
                table: "Empresa",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaQSA_QSAsId",
                table: "EmpresaQSA",
                column: "QSAsId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_EmpresaId",
                table: "Inscricao",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_EventoId",
                table: "Inscricao",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_ParticipanteId",
                table: "Inscricao",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CPF",
                table: "Pessoa",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SecretariaId",
                table: "Servico",
                column: "SecretariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SetorId",
                table: "Servico",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_SecretariaId",
                table: "Setor",
                column: "SecretariaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimento");

            migrationBuilder.DropTable(
                name: "Canal");

            migrationBuilder.DropTable(
                name: "DIA");

            migrationBuilder.DropTable(
                name: "EmpresaQSA");

            migrationBuilder.DropTable(
                name: "Inscricao");

            migrationBuilder.DropTable(
                name: "Planer");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Ambulante");

            migrationBuilder.DropTable(
                name: "QSA");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Secretaria");
        }
    }
}
