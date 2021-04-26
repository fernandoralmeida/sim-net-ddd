using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Context
{
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.Shared.Entity;
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {  }

        //SDE
        public DbSet<Ambulante> Ambulante { get; set; }
        public DbSet<DIA> DIA { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<QSA> QSA { get; set; }

        //Shared
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<Canal> Canal { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Planer> Planer { get; set; }
        public DbSet<Secretaria> Secretaria { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Setor> Setor { get; set; }
        public DbSet<Inscricao> Inscricao { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ambulante>().ToTable("Ambulante");
            modelBuilder.Entity<DIA>().ToTable("DIA");
            modelBuilder.Entity<Pessoa>().ToTable("Pessoa");
            modelBuilder.Entity<Empresa>().ToTable("Empresa");
            modelBuilder.Entity<QSA>().ToTable("QSA");
            modelBuilder.Entity<Atendimento>().ToTable("Atendimento");            
            modelBuilder.Entity<Canal>().ToTable("Canal");
            modelBuilder.Entity<Evento>().ToTable("Evento");
            modelBuilder.Entity<Planer>().ToTable("Planer");
            modelBuilder.Entity<Secretaria>().ToTable("Secretaria");
            modelBuilder.Entity<Setor>().ToTable("Setor");
            modelBuilder.Entity<Servico>().ToTable("Servico");
            modelBuilder.Entity<Inscricao>().ToTable("Inscricao");
            modelBuilder.Entity<Tipo>().ToTable("Tipos");

            modelBuilder.ApplyConfiguration(new Config.Entity.AmbulanteMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.AtendimentoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.CanalMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.DIAMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.EmpresaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.PessoaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.PlanerMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.QSAMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.SecretariaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.ServicoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.SetorMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.InscricaoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.TipoMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Data_Cadastro") != null))
            {

                if (entry.State == EntityState.Added)
                    entry.Property("Data_Cadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("Data_Cadastro").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Ultima_Alteracao") != null))
            {
                entry.Property("Ultima_Alteracao").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }

    }
}
