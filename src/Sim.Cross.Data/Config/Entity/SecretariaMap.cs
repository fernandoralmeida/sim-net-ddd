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
    public class SecretariaMap : IEntityTypeConfiguration<Secretaria>
    {
        public void Configure(EntityTypeBuilder<Secretaria> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Owner)
                .HasColumnType("varchar(max)");
        }
    }
}
