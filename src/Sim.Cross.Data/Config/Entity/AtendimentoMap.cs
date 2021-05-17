﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sim.Cross.Data.Config.Entity
{
    
    using Sim.Domain.Shared.Entity;
    public class AtendimentoMap : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Protocolo)
                .HasColumnType("varchar(256)");
            builder.HasIndex(c => c.Protocolo).IsUnique();
            builder.Property(c => c.Protocolo)
                .IsRequired();

            builder.Property(c => c.Setor)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Canal)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Servicos)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Status)
                .HasColumnType("varchar(max)");

        }
    }
}
