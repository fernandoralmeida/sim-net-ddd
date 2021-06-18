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


    using Sim.Application.Shared;
    using Sim.Application.Shared.Interface;
    using Sim.Application.Shared.Service;

    using Sim.Application.Interface;

    using Sim.Application.SDE;
    using Sim.Application.SDE.Interface;
    using Sim.Application;

    using Sim.Cross.Data.Repository.SDE;
    using Sim.Cross.Data.Repository.Shared;

    using Sim.Cross.Data.Repository.Cnpj;
    using Sim.Domain.Cnpj.Interface;
    using Sim.Domain.Cnpj.Entity;

    using Sim.Domain.Cnpj.Service;


    public static class Container
    {
        public static void RegisterApplicationService(IServiceCollection services)
        {
            RegisterPessoa(services);
            RegisterEmpresa(services);
            RegisterAtendimento(services);
            RegisterSecretaria(services);
            RegisterSetor(services);
            RegisterServico(services);
            RegisterEvento(services);
            RegisterCanal(services);
            RegisterContador(services);
            RegisterTiposEventos(services);
            RegisterTiposInscricaoEventos(services);
        }

        private static void RegisterPessoa(IServiceCollection services)
        {           

            //registra o aplicação, dominio, repositorio aos serviços.
            services.AddScoped<IAppServiceBase<Pessoa>, AppServiceBase<Pessoa>>();
            services.AddScoped<IAppServicePessoa, AppServicePessoa>();

            services.AddScoped<IServiceBase<Pessoa>, ServiceBase<Pessoa>>();
            services.AddScoped<IServicePessoa, ServicePessoa>();

            services.AddScoped<IRepositoryBase<Pessoa>, RepositoryBase<Pessoa>>();
            services.AddScoped<IRepositoryPessoa, RepositoryPessoa>();
        }

        private static void RegisterEmpresa(IServiceCollection services)
        {

            services.AddScoped<ICNPJBase<BaseReceitaFederal>, RepositoryRFB>();
            services.AddScoped<ICNPJBase<BaseJucesp>, RepositoryJucesp>();
            services.AddScoped<IBase<CNAE>, RepositoryCNAEs>();
            services.AddScoped<IBase<Municipio>, RepositoryMunicipios>();

            services.AddScoped<IServiceCnpj<BaseReceitaFederal>, ServiceRFB>();

            /**/
            services.AddScoped<IAppServiceBase<Domain.SDE.Entity.Empresa>, AppServiceBase<Domain.SDE.Entity.Empresa>>();
            services.AddScoped<IAppServiceEmpresa, AppServiceEmpresa>();
            services.AddScoped<IAppServiceBase<QSA>, AppServiceBase<QSA>>();
            services.AddScoped<IAppServiceQSA, AppServiceQSA>();

            services.AddScoped<IServiceBase<Domain.SDE.Entity.Empresa>, ServiceBase<Domain.SDE.Entity.Empresa>>();
            services.AddScoped<IServiceEmpresa, ServiceEmpresa>();
            services.AddScoped<IServiceBase<QSA>, ServiceBase<QSA>>();
            services.AddScoped<IServiceQSA, ServiceQSA>();

            services.AddScoped<IRepositoryBase<Domain.SDE.Entity.Empresa>, RepositoryBase<Domain.SDE.Entity.Empresa>>();
            services.AddScoped<IRepositoryEmpresa, RepositoryEmpresa>();
            services.AddScoped<IRepositoryBase<QSA>, RepositoryBase<QSA>>();
            services.AddScoped<IRepositoryQSA, RepositoryQsa>();
            
        }

        private static void RegisterAtendimento(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Atendimento>, AppServiceBase<Atendimento>>();
            services.AddScoped<IAppServiceAtendimento, AppServiceAtendimento>();

            services.AddScoped<IServiceBase<Atendimento>, ServiceBase<Atendimento>>();
            services.AddScoped<IServiceAtendimento, ServiceAtendimento>();

            services.AddScoped<IRepositoryBase<Atendimento>, RepositoryBase<Atendimento>>();
            services.AddScoped<IRepositoryAtendimento, RepositoryAtendimento>();
        }

        private static void RegisterSecretaria(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Secretaria>, AppServiceBase<Secretaria>>();
            services.AddScoped<IAppServiceSecretaria, AppServiceSecretaria>();

            services.AddScoped<IServiceBase<Secretaria>, ServiceBase<Secretaria>>();
            services.AddScoped<IServiceSecretaria, ServiceSecretaria>();

            services.AddScoped<IRepositoryBase<Secretaria>, RepositoryBase<Secretaria>>();
            services.AddScoped<IRepositorySecretaria, RepositorySecretaria>();
        }

        private static void RegisterSetor(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Setor>, AppServiceBase<Setor>>();
            services.AddScoped<IAppServiceSetor, AppServiceSetor>();

            services.AddScoped<IServiceBase<Setor>, ServiceBase<Setor>>();
            services.AddScoped<IServiceSetor, ServiceSetor>();

            services.AddScoped<IRepositoryBase<Setor>, RepositoryBase<Setor>>();
            services.AddScoped<IRepositorySetor, RepositorySetor>();
        }

        private static void RegisterServico(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Servico>, AppServiceBase<Servico>>();
            services.AddScoped<IAppServiceServico, AppServiceServico>();

            services.AddScoped<IServiceBase<Servico>, ServiceBase<Servico>>();
            services.AddScoped<IServiceServico, ServiceServico>();

            services.AddScoped<IRepositoryBase<Servico>, RepositoryBase<Servico>>();
            services.AddScoped<IRepositoryServico, RepositoryServico>();
        }

        private static void RegisterEvento(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Evento>, AppServiceBase<Evento>>();
            services.AddScoped<IAppServiceEvento, AppServiceEvento>();

            services.AddScoped<IServiceBase<Evento>, ServiceBase<Evento>>();
            services.AddScoped<IServiceEvento, ServiceEvento>();

            services.AddScoped<IRepositoryBase<Evento>, RepositoryBase<Evento>>();
            services.AddScoped<IRepositoryEvento, RepositoryEvento>();
        }

        private static void RegisterCanal(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Canal>, AppServiceBase<Canal>>();
            services.AddScoped<IAppServiceCanal, AppServiceCanal>();

            services.AddScoped<IServiceBase<Canal>, ServiceBase<Canal>>();
            services.AddScoped<IServiceCanal, ServiceCanal>();

            services.AddScoped<IRepositoryBase<Canal>, RepositoryBase<Canal>>();
            services.AddScoped<IRepositoryCanal, RepositoryCanal>();
        }

        private static void RegisterContador(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Contador>, AppServiceBase<Contador>>();
            services.AddScoped<IAppServiceContador, AppServiceContador>();

            services.AddScoped<IServiceBase<Contador>, ServiceBase<Contador>>();
            services.AddScoped<IServiceContador, ServiceContador>();

            services.AddScoped<IRepositoryBase<Contador>, RepositoryBase<Contador>>();
            services.AddScoped<IRepositoryContador, RepositoryContador>();
        }

        private static void RegisterTiposEventos(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Tipo>, AppServiceBase<Tipo>>();
            services.AddScoped<IAppServiceTipo, AppServiceTipo>();

            services.AddScoped<IServiceBase<Tipo>, ServiceBase<Tipo>>();
            services.AddScoped<IServiceTipo, ServiceTipos>();

            services.AddScoped<IRepositoryBase<Tipo>, RepositoryBase<Tipo>>();
            services.AddScoped<IRepositoryTipo, RepositoryTipo>();
        }

        private static void RegisterTiposInscricaoEventos(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Inscricao>, AppServiceBase<Inscricao>>();
            services.AddScoped<IAppServiceInscricao, AppServiceInscricao>();

            services.AddScoped<IServiceBase<Inscricao>, ServiceBase<Inscricao>>();
            services.AddScoped<IServiceInscricao, ServiceInscricao>();

            services.AddScoped<IRepositoryBase<Inscricao>, RepositoryBase<Inscricao>>();
            services.AddScoped<IRepositoryInscricao, RepositoryInscricao>();
        }
    }
}
