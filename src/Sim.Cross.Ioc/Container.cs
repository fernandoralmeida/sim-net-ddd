using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Sim.Cross.Ioc
{
    //using Sim.Cross.Identity;
    using Sim.Cross.Data.Context;
    using Sim.Cross.Data.Repository;

    using Sim.Domain.SDE.Entity;
    using Sim.Domain.SDE.Interface;
    using Sim.Domain.SDE.Service;

    using Sim.Domain.Shared.Entity;
    using Sim.Domain.Shared.Interface;
    using Sim.Domain.Shared.Service;
    using Sim.Domain.Interface;
    using Sim.Domain.Service;

    using Sim.Application.Interface;
    using Sim.Application.Shared;
    using Sim.Application.Shared.Interface;

    using Sim.Application.SDE;
    using Sim.Application.SDE.Interface;
    using Sim.Application;

    using Sim.Cross.Data.Repository.SDE;
    using Sim.Cross.Data.Repository.Shared;

    public class Container
    {

        public void RegisterApplicationService(IServiceCollection services, IConfiguration config, string connection)
        {
            //registra o dbcontext aos serviços
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(config
                .GetConnectionString(connection)));

            services.AddScoped<DbContext, ApplicationContext>();

            RegisterPessoa(services);
            RegisterEmpresa(services);
            //RegisterAtendimento(services);
        }

        private void RegisterPessoa(IServiceCollection services)
        {           

            //registra o aplicação, dominio, repositorio aos serviços.
            services.AddScoped<IAppServiceBase<Pessoa>, AppServiceBase<Pessoa>>();
            services.AddScoped<IAppServicePessoa, AppServicePessoa>();

            services.AddScoped<IServiceBase<Pessoa>, ServiceBase<Pessoa>>();
            services.AddScoped<IServicePessoa, ServicePessoa>();

            services.AddScoped<IRepositoryBase<Pessoa>, RepositoryBase<Pessoa>>();
            services.AddScoped<IRepositoryPessoa, RepositoryPessoa>();
        }

        private void RegisterEmpresa(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Empresa>, AppServiceBase<Empresa>>();
            services.AddScoped<IAppServiceEmpresa, AppServiceEmpresa>();

            services.AddScoped<IServiceBase<Empresa>, ServiceBase<Empresa>>();
            services.AddScoped<IServiceEmpresa, ServiceEmpresa>();

            services.AddScoped<IRepositoryBase<Empresa>, RepositoryBase<Empresa>>();
            services.AddScoped<IRepositoryEmpresa, RepositoryEmpresa>();
        }

        private void RegisterAtendimento(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Atendimento>, AppServiceBase<Atendimento>>();
            services.AddScoped<IAppServiceAtendimento, AppServiceAtendimento>();

            services.AddScoped<IServiceBase<Atendimento>, ServiceBase<Atendimento>>();
            services.AddScoped<IServiceAtendimento, ServiceAtendimento>();

            services.AddScoped<IRepositoryBase<Atendimento>, RepositoryBase<Atendimento>>();
            services.AddScoped<IRepositoryAtendimento, RepositoryAtendimento>();
        }
    }
}
