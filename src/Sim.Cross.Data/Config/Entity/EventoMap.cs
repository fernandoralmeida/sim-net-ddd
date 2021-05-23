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
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Tipo)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Owner)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Formato)
                .HasColumnType("varchar(255)");
            builder.Property(c => c.Estado)
                .HasColumnType("varchar(255)");
        }
    }
}
