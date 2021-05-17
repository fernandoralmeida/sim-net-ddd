﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sim.Cross.Data.Config.Entity
{
    using Domain.SDE.Entity;
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Nome_Social)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Data_Nascimento)
                .IsRequired();

            builder.HasIndex(c => c.CPF).IsUnique();
            builder.Property(c => c.CPF)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(c => c.RG)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.RG_Emissor)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.RG_Emissor_UF)
                .HasColumnType("varchar(2)");

            builder.Property(c => c.CEP)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Logradouro)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Numero)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Complemento)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Bairro)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Cidade)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.UF)
                .HasColumnType("varchar(2)");

            builder.Property(c => c.Tel_Fixo)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Tel_Movel)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Email)
                .HasColumnType("varchar(max)");

        }
    }
}
