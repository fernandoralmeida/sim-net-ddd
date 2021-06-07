using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sim.Cross.Data.Config.Cnpj
{
    using Sim.Domain.Cnpj.Entity;
    public class PaisMap : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.Codigo)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
        }
    }
}
