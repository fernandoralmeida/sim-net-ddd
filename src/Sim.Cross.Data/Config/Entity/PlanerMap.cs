using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sim.Cross.Data.Config.Entity
{
    using Sim.Domain.Shared.Entity;
    public class PlanerMap : IEntityTypeConfiguration<Planer>
    {
        public void Configure(EntityTypeBuilder<Planer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Segunda)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Terca)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Quarta)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Quinta)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Sexta)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Sabado)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.ProximaSemana)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Prioridades)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Anotacao)
                .HasColumnType("varchar(max)");
        }
    }
}
