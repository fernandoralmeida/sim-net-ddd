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
    public class AtendimentoMap : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Protocolo).IsUnique();
            builder.Property(c => c.Protocolo)
                .IsRequired();

            builder.Property(c => c.Setor)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Canal)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.Servicos)
                .HasColumnType("varchar(150)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(150)");
            builder.Property(c => c.Status)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.UserName)
                .HasColumnType("varchar(256)");

            builder
                .HasOne(c => c.Empresa)
                .WithMany(c => c.Atendimento)
                .HasForeignKey(p => p.Empresa_Id)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Pessoa)
                .WithMany(c => c.Atendimento)
                .HasForeignKey(p => p.Pessoa_Id)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
