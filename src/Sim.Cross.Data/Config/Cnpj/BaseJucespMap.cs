using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Sim.Cross.Data.Config.Cnpj
{
    using Sim.Domain.Cnpj.Entity;
    public class BaseJucespMap : IEntityTypeConfiguration<BaseJucesp>
    {
        public void Configure(EntityTypeBuilder<BaseJucesp> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.Inscricao_Estadual)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.CNPJ)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Nome_Empresarial)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Nome_Fantasia)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Natureza_Juridica)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Tipo_Logradouro)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Nome_Logradouro)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Numero_Logradouro)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Complemento_Logradouro)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.CEP)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Bairro)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Municipio)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.UF)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Situacao_Cadastral)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Data_Situacao_Cadastral)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Ocorrencia_Fiscal)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Regime_Apuracao)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Atividade_Economica)
                .HasColumnType("varchar(max)");
        }
    }
}
