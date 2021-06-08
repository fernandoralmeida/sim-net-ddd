using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Sim.Domain.Cnpj.Entity;

namespace Sim.Cross.Data.Context
{    
    using Config.Cnpj;
    

    public class RFBContext : DbContext
    {
        public RFBContext() { }
        public RFBContext(DbContextOptions<RFBContext> options) : base(options) { }
        
        public DbSet<CNAE> CNAEs { get; set; }

        /// <summary>
        /// Previsão para futuras atualizaçoes
        /// por hora usarei o modelo simples importado sem tratamento da receita federal.
        /// </summary>
        //public DbSet<BaseReceitaFederal> BaseReceitaFederal { get; set; }
        //public DbSet<CNAEPrincipal> CNAEPrincipais { get; set; }
        //public DbSet<CNAESecundaria> CNAESecundarias { get; set; }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<MotivoSituacaoCadastral> MotivoSituacaoCadastral { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<NaturezaJuridica> NaturezaJuridica { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<QualificacaoSocio> QualificacaoSocios { get; set; }
        public DbSet<Simples> Simples { get; set; }
        public DbSet<Socio> Socios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlServerConnect());
            }
            /*get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("RFB_____ContextConnection"));*/
        }
        private static string _sqlserverconnect;
        private static string SqlServerConnect()
        {
            return _sqlserverconnect;
        }
        public void RegisterDataContext(IServiceCollection services, IConfiguration config, string connection)
        {
            _sqlserverconnect = config.GetConnectionString(connection);

            services.AddDbContext<RFBContext>(options => options.UseSqlServer(config.GetConnectionString(connection)));

            services.AddScoped<DbContext, RFBContext>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new BaseReceitaFederalMap());
            modelBuilder.ApplyConfiguration(new CnaeMap());
            //modelBuilder.ApplyConfiguration(new CnaePrincipalMap());
            //modelBuilder.ApplyConfiguration(new CnaeSencundariasMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new EstabelecimentoMap());
            modelBuilder.ApplyConfiguration(new MotivoSituacaoCadastralMap());
            modelBuilder.ApplyConfiguration(new MunicipioMap());
            modelBuilder.ApplyConfiguration(new NaturezaJuridicaMap());
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new QualificacaoSocioMap());
            modelBuilder.ApplyConfiguration(new SimplesMap());
            modelBuilder.ApplyConfiguration(new SociosMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
