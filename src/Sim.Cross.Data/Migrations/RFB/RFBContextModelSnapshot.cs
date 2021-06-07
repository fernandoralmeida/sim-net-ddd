﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sim.Cross.Data.Context;

namespace Sim.Cross.Data.Migrations.RFB
{
    [DbContext(typeof(RFBContext))]
    partial class RFBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.CNAE", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.ToTable("CNAEs");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.Empresa", b =>
                {
                    b.Property<string>("CNPJBase")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CapitalSocial")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EnteFederativoResponsavel")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NaturezaJuridica")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PorteEmpresa")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("QualificacaoResponsavel")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("varchar(255)");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.Estabelecimento", b =>
                {
                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CNPJBase")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CNPJDV")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("CNPJOrdem")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("CnaeFiscalPrincipal")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CnaeFiscalSecundaria")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CorreioEletronico")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DDD1")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("DDD2")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("DDDFax")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("DataInicioAtividade")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DataSituacaoCadastral")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DataSitucaoEspecial")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Fax")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IdentificadorMatrizFilial")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MotivoSituacaoCadastral")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Municipio")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("NomeCidadeExterior")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Pais")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("SituacaoCadastral")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("SituacaoEspecial")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Telefone1")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Telefone2")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TipoLogradouro")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.ToTable("Estabelecimentos");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.MotivoSituacaoCadastral", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.ToTable("MotivoSituacaoCadastral");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.Municipio", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.NaturezaJuridica", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.ToTable("NaturezaJuridica");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.Pais", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.QualificacaoSocio", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.ToTable("QualificacaoSocios");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.Simples", b =>
                {
                    b.Property<string>("CNPJBase")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DataExclusaoMEI")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DataExclusaoSimples")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DataOpcaoMEI")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DataOpcaoSimples")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("OpcaoMEI")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("OpcaoSimples")
                        .HasColumnType("varchar(2)");

                    b.ToTable("Simples");
                });

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.Socio", b =>
                {
                    b.Property<string>("CNPJBase")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CnpjCpfSocio")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DataEntradaSociedade")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FaixaEtaria")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("IdentificadorSocio")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("NomeRazaoSocio")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NomeRepresentante")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Pais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualificacaoRepresentanteLegal")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("QualificacaoSocio")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("RepresentanteLegal")
                        .HasColumnType("varchar(50)");

                    b.ToTable("Socios");
                });
#pragma warning restore 612, 618
        }
    }
}
