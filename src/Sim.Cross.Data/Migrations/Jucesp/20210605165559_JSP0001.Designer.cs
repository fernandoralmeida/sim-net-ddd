// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sim.Cross.Data.Context;

namespace Sim.Cross.Data.Migrations.Jucesp
{
    [DbContext(typeof(JucespContext))]
    [Migration("20210605165559_JSP0001")]
    partial class JSP0001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sim.Domain.Cnpj.Entity.BaseJucesp", b =>
                {
                    b.Property<string>("Atividade_Economica")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CNPJ")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Complemento_Logradouro")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Data_Situacao_Cadastral")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Inscricao_Estadual")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Municipio")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Natureza_Juridica")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome_Empresarial")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome_Fantasia")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome_Logradouro")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Numero_Logradouro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ocorrencia_Fiscal")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Regime_Apuracao")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Situacao_Cadastral")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Tipo_Logradouro")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.ToTable("BaseJucesp");
                });
#pragma warning restore 612, 618
        }
    }
}
