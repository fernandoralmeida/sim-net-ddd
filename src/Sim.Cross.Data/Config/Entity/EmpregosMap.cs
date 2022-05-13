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
    public class EmpregosMap: IEntityTypeConfiguration<Empregos>
    {
        public void Configure(EntityTypeBuilder<Empregos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Ocupacao)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Salario)
                .HasColumnType("varchar(max)");

        }
    }
}
