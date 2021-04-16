﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Config.Entity
{
    using Sim.Domain.SDE.Entity;
    public class QSAMap : IEntityTypeConfiguration<QSA>
    {
        public void Configure(EntityTypeBuilder<QSA> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Nome)
                .HasColumnType("varchar(150)");

            builder
                .Property(c => c.Qualificacao)
                .HasColumnType("varchar(50)");

            builder
                .HasOne(c => c.Empresa)
                .WithMany(c => c.QSA)
                .HasForeignKey(p => p.Empresa_Id)
                .HasPrincipalKey(p => p.Id);

        }
    }
}