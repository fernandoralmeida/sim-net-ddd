﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sim.Cross.Data.Context;

namespace Sim.Cross.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210524022029_AP0006")]
    partial class AP0006
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AmbulantePessoa", b =>
                {
                    b.Property<Guid>("AmbulanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PessoasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AmbulanteId", "PessoasId");

                    b.HasIndex("PessoasId");

                    b.ToTable("AmbulantePessoa");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Ambulante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Atividade")
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Data_Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormaAtuacao")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Local")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Protocolo")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Protocolo")
                        .IsUnique();

                    b.ToTable("Ambulante");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.DIA", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AmbulanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Autorizacao")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("DiaDesde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Emissao")
                        .HasColumnType("datetime2");

                    b.Property<int>("InscricaoMunicipal")
                        .HasColumnType("int");

                    b.Property<string>("Processo")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Situacao")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Validade")
                        .HasColumnType("datetime2");

                    b.Property<string>("Veiculo")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AmbulanteId");

                    b.HasIndex("Autorizacao")
                        .IsUnique();

                    b.ToTable("DIA");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Atividade_Principal")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Atividade_Secundarias")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CNAE_Principal")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.Property<decimal>("Capital_Social")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Data_Abertura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Situacao_Cadastral")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data_Situacao_Especial")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Ente_Federativo_Resp")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Motivo_Situacao_Cadastral")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Municipio")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Natureza_Juridica")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome_Empresarial")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome_Fantasia")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Porte")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Situacao_Cadastral")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Situacao_Especial")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("CNPJ")
                        .IsUnique();

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Data_Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Nascimento")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Deficiencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome_Social")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("RG")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("RG_Emissor")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("RG_Emissor_UF")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Tel_Fixo")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Tel_Movel")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.QSA", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("NomeRepLegal")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("PaisOrigem")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Qual")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("QualRepLegal")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("QSA");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.RaeSebrae", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RAE")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RaeSebrae");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Atendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Canal")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataF")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Owner_AppUser_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Protocolo")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<Guid?>("SebraeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Servicos")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Setor")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("PessoaId");

                    b.HasIndex("Protocolo")
                        .IsUnique();

                    b.HasIndex("SebraeId");

                    b.ToTable("Atendimento");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Canal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<Guid?>("SecretariaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SetorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SecretariaId");

                    b.HasIndex("SetorId");

                    b.ToTable("Canal");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Contador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Modulo")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Protocolos");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Evento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Formato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lotacao")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Inscricao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AplicationUser_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Data_Inscricao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EventoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParticipanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Presente")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("EventoId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("Inscricao");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Planer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Anotacao")
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Owner_AppUser_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prioridades")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ProximaSemana")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Quarta")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Quinta")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Sabado")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Segunda")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Sexta")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Terca")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Planer");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Secretaria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Secretaria");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Servico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<Guid?>("SecretariaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SetorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SecretariaId");

                    b.HasIndex("SetorId");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Setor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<Guid?>("SecretariaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SecretariaId");

                    b.ToTable("Setor");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Tipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tipos");
                });

            modelBuilder.Entity("AmbulantePessoa", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Ambulante", null)
                        .WithMany()
                        .HasForeignKey("AmbulanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sim.Domain.SDE.Entity.Pessoa", null)
                        .WithMany()
                        .HasForeignKey("PessoasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.DIA", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Ambulante", "Ambulante")
                        .WithMany("DIAs")
                        .HasForeignKey("AmbulanteId");

                    b.Navigation("Ambulante");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.QSA", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Empresa", null)
                        .WithMany("QSA")
                        .HasForeignKey("EmpresaId");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Atendimento", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Empresa", "Empresa")
                        .WithMany("Atendimentos")
                        .HasForeignKey("EmpresaId");

                    b.HasOne("Sim.Domain.SDE.Entity.Pessoa", "Pessoa")
                        .WithMany("Atendimentos")
                        .HasForeignKey("PessoaId");

                    b.HasOne("Sim.Domain.SDE.Entity.RaeSebrae", "Sebrae")
                        .WithMany()
                        .HasForeignKey("SebraeId");

                    b.Navigation("Empresa");

                    b.Navigation("Pessoa");

                    b.Navigation("Sebrae");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Canal", b =>
                {
                    b.HasOne("Sim.Domain.Shared.Entity.Secretaria", "Secretaria")
                        .WithMany("Canais")
                        .HasForeignKey("SecretariaId");

                    b.HasOne("Sim.Domain.Shared.Entity.Setor", "Setor")
                        .WithMany("Canais")
                        .HasForeignKey("SetorId");

                    b.Navigation("Secretaria");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Inscricao", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Empresa", "Empresa")
                        .WithMany("Inscricoes")
                        .HasForeignKey("EmpresaId");

                    b.HasOne("Sim.Domain.Shared.Entity.Evento", "Evento")
                        .WithMany("Inscritos")
                        .HasForeignKey("EventoId");

                    b.HasOne("Sim.Domain.SDE.Entity.Pessoa", "Participante")
                        .WithMany("Inscricoes")
                        .HasForeignKey("ParticipanteId");

                    b.Navigation("Empresa");

                    b.Navigation("Evento");

                    b.Navigation("Participante");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Servico", b =>
                {
                    b.HasOne("Sim.Domain.Shared.Entity.Secretaria", "Secretaria")
                        .WithMany("Servicos")
                        .HasForeignKey("SecretariaId");

                    b.HasOne("Sim.Domain.Shared.Entity.Setor", "Setor")
                        .WithMany("Servicos")
                        .HasForeignKey("SetorId");

                    b.Navigation("Secretaria");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Setor", b =>
                {
                    b.HasOne("Sim.Domain.Shared.Entity.Secretaria", "Secretaria")
                        .WithMany("Setores")
                        .HasForeignKey("SecretariaId");

                    b.Navigation("Secretaria");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Ambulante", b =>
                {
                    b.Navigation("DIAs");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Empresa", b =>
                {
                    b.Navigation("Atendimentos");

                    b.Navigation("Inscricoes");

                    b.Navigation("QSA");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Pessoa", b =>
                {
                    b.Navigation("Atendimentos");

                    b.Navigation("Inscricoes");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Evento", b =>
                {
                    b.Navigation("Inscritos");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Secretaria", b =>
                {
                    b.Navigation("Canais");

                    b.Navigation("Servicos");

                    b.Navigation("Setores");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Setor", b =>
                {
                    b.Navigation("Canais");

                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}
