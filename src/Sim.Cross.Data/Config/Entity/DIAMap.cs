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
    public class DIAMap : IEntityTypeConfiguration<DIA>
    {
        public void Configure(EntityTypeBuilder<DIA> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Autorizacao).IsUnique();
            builder.Property(c => c.Autorizacao)
                .HasColumnType("varchar(256)").IsRequired();
            /*
            builder.Property(c => c.Atividade)
                .HasColumnType("varchar(256)");
            builder.Property(c => c.FormaAtuacao)
                .HasColumnType("varchar(150)");
            */
            builder.Property(c => c.Veiculo)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Processo)
                .HasColumnType("varchar(15)");
            builder.Property(c => c.Situacao)
                .HasColumnType("varchar(20)");
        }
    }
}
