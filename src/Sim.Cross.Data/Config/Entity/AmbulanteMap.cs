using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sim.Cross.Data.Config.Entity
{
    using Sim.Domain.SDE.Entity;
    public class AmbulanteMap : IEntityTypeConfiguration<Ambulante>
    {
        public void Configure(EntityTypeBuilder<Ambulante> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Protocolo).IsUnique();
            builder.Property(c => c.Protocolo)
                .HasColumnType("varchar(256)")
                .IsRequired();
            builder.Property(c => c.Titular)
                .HasColumnType("varchar(256)");
            builder.Property(c => c.Auxiliar)
                .HasColumnType("varchar(256)");
            builder.Property(c => c.FormaAtuacao)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Local)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Atividade)
                .HasColumnType("varchar(256)");

        }
    }
}
