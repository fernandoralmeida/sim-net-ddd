using Microsoft.EntityFrameworkCore;
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
                .HasColumnType("varchar(max)");

            builder
                .Property(c => c.Qual)
                .HasColumnType("varchar(max)");
            builder
                .Property(c => c.QualRepLegal)
                .HasColumnType("varchar(max)");
            builder
                .Property(c => c.NomeRepLegal)
                .HasColumnType("varchar(max)");
            builder
                .Property(c => c.PaisOrigem)
                .HasColumnType("varchar(max)");

        }
    }
}
