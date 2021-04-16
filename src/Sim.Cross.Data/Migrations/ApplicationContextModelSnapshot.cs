﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sim.Cross.Data.Context;

namespace Sim.Cross.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Ambulante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Atividade")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Auxiliar")
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Contador")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Data_Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormaAtuacao")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Local")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("PessoaId")
                        .HasColumnType("int");

                    b.Property<string>("Protocolo")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Titular")
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.HasIndex("Protocolo")
                        .IsUnique();

                    b.ToTable("Ambulante");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.DIA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Atividade")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Autorizacao")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Auxiliar")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Contador")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DiaDesde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Emissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormaAtuacao")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("InscricaoMunicipal")
                        .HasColumnType("int");

                    b.Property<string>("Processo")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Situacao")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Titular")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime?>("Validade")
                        .HasColumnType("datetime2");

                    b.Property<string>("Veiculo")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Autorizacao")
                        .IsUnique();

                    b.ToTable("DIA");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Atividade_Principal")
                        .HasColumnType("varchar(999)");

                    b.Property<string>("Atividade_Secundarias")
                        .HasColumnType("varchar(999)");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CNAE_Principal")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.Property<decimal>("Capital_Social")
                        .HasColumnType("decimal");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("Data_Abertura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Situacao_Cadastral")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Situacao_Especial")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Ente_Federativo_Resp")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Motivo_Situacao_Cadastral")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Municipio")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Natureza_Juridica")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome_Empresarial")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome_Fantasia")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Porte")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Situacao_Cadastral")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Situacao_Especial")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Tipo")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("CNPJ")
                        .IsUnique();

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("Data_Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Nascimento")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Deficiencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome_Social")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("RG")
                        .HasColumnType("varchar(12)");

                    b.Property<string>("RG_Emissor")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("RG_Emissor_UF")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Tel_Fixo")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Tel_Movel")
                        .HasColumnType("varchar(15)");

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Empresa_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Qualificacao")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Empresa_Id");

                    b.ToTable("QSA");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Atendimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Canal")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Empresa_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Fim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("Pessoa_Id")
                        .HasColumnType("int");

                    b.Property<int>("Protocolo")
                        .HasColumnType("int");

                    b.Property<string>("Servicos")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Setor")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Empresa_Id");

                    b.HasIndex("Pessoa_Id");

                    b.HasIndex("Protocolo")
                        .IsUnique();

                    b.ToTable("Atendimento");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Canal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Canal");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Inscritos")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Data_Inscricao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Empresa_Id")
                        .HasColumnType("int");

                    b.Property<int>("Evento_Id")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Pessoa_Id")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Inscricao");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Planer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anotacao")
                        .HasColumnType("varchar(1000)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Prioridades")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("ProximaSemana")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Quarta")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Quinta")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Sabado")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Segunda")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Sexta")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Terca")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Planer");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Secretaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Secretaria");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Setor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Setor");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Ambulante", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Pessoa", null)
                        .WithMany("Ambulante")
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.QSA", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Empresa", "Empresa")
                        .WithMany("QSA")
                        .HasForeignKey("Empresa_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Sim.Domain.Shared.Entity.Atendimento", b =>
                {
                    b.HasOne("Sim.Domain.SDE.Entity.Empresa", "Empresa")
                        .WithMany("Atendimento")
                        .HasForeignKey("Empresa_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sim.Domain.SDE.Entity.Pessoa", "Pessoa")
                        .WithMany("Atendimento")
                        .HasForeignKey("Pessoa_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Empresa", b =>
                {
                    b.Navigation("Atendimento");

                    b.Navigation("QSA");
                });

            modelBuilder.Entity("Sim.Domain.SDE.Entity.Pessoa", b =>
                {
                    b.Navigation("Ambulante");

                    b.Navigation("Atendimento");
                });
#pragma warning restore 612, 618
        }
    }
}