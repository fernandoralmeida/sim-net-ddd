using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Sim.Cross.Data.Config.Cnpj
{
    using Sim.Domain.Cnpj.Entity;
    public class CnaeMap : IEntityTypeConfiguration<CNAE>
    {
        public void Configure(EntityTypeBuilder<CNAE> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.Codigo)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
        }
    }
}
