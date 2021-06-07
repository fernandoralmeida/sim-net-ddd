﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sim.Cross.Data.Config.Cnpj
{
    using Sim.Domain.Cnpj.Entity;
    public class SimplesMap : IEntityTypeConfiguration<Simples>
    {
        public void Configure(EntityTypeBuilder<Simples> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.CNPJBase)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.OpcaoSimples)
                .HasColumnType("varchar(2)");
            builder.Property(c => c.DataOpcaoSimples)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.DataExclusaoSimples)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.OpcaoMEI)
                .HasColumnType("varchar(2)");
            builder.Property(c => c.DataOpcaoMEI)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.DataExclusaoMEI)
                .HasColumnType("varchar(10)");
        }
    }
}
