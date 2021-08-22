using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sim.Cross.Data.Config.Entity
{
    using Sim.Domain.Shared.Entity;
    class StatusAtendimentoMap : IEntityTypeConfiguration<StatusAtendimento>
    {
        public void Configure(EntityTypeBuilder<StatusAtendimento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UnserName)
                .HasColumnType("varchar(256)");
        }
    }
}
