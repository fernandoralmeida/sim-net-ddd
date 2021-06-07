using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using Sim.Domain.Cnpj.Entity;

namespace Sim.Cross.Data.Context
{
    using Config.Cnpj;
    public class JucespContext : DbContext
    {
        public JucespContext() { }
        public JucespContext(DbContextOptions options) : base(options) { }
        public DbSet<BaseJucesp> BaseJucesp { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("Jucesp__ContextConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BaseJucespMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
