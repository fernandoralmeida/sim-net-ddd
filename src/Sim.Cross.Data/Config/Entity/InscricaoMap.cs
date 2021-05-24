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

    class InscricaoMap: IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
