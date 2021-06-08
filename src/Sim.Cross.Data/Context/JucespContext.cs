using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

using Sim.Domain.Cnpj.Entity;

namespace Sim.Cross.Data.Context
{
    using Config.Cnpj;
    public class JucespContext : DbContext
    {
        public JucespContext() { }
        public JucespContext(DbContextOptions<JucespContext> options) : base(options) { }
        public DbSet<BaseJucesp> BaseJucesp { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("Jucesp__ContextConnection"));*/

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlServerConnect());
            }
        }
        private static string _sqlserverconnect;
        private static string SqlServerConnect()
        {
            return _sqlserverconnect;
        }
        public void RegisterDataContext(IServiceCollection services, IConfiguration config, string connection)
        {
            _sqlserverconnect = config.GetConnectionString(connection);

            services.AddDbContext<JucespContext>(options => options.UseSqlServer(config.GetConnectionString(connection)));

            services.AddScoped<DbContext, JucespContext>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BaseJucespMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
