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
    public class ContadorMap : IEntityTypeConfiguration<Contador>
    {
        public void Configure(EntityTypeBuilder<Contador> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Numero)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GeradorProtocolo>()
                .HasColumnType("varchar(max)"); 

            builder
                .Property(c => c.Modulo)
                .HasColumnType("varchar(max)");

            builder
                .Property(c => c.AppUserId)
                .HasColumnType("nvarchar(max)");
        }

    }


}
