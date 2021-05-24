using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Sim.UI.Web.AutoMapper
{
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.Shared.Entity;
    using Web.Pages.Pessoa;
    using Web.Pages.Empresa;
    using Sim.Service.CNPJ.Entity;
    using Web.Pages.Agenda;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pages.Pessoa.NovoModel.InputModel, Pessoa>();
            CreateMap<Pessoa, Pages.Pessoa.NovoModel.InputModel>().ReverseMap();

            CreateMap<Pages.Pessoa.EditModel.InputModel, Pessoa>();
            CreateMap<Pessoa, Pages.Pessoa.EditModel.InputModel>().ReverseMap();

            CreateMap<VMEmpresa, CNPJ>();
            CreateMap<CNPJ, VMEmpresa>().ReverseMap();

            CreateMap<Qsa, QSA>();
            CreateMap<QSA, Qsa>().ReverseMap();

            CreateMap<VMEmpresa, Empresa>();
            CreateMap<Empresa, VMEmpresa>().ReverseMap();

            CreateMap<Pages.Atendimento.InputModel, Atendimento>();
            CreateMap<Atendimento, Pages.Atendimento.InputModel>().ReverseMap();

            CreateMap<InputModelEvento, Evento>();
            CreateMap<Evento, InputModelEvento>().ReverseMap();

            CreateMap<InputModelInscricao, Inscricao>();
            CreateMap<Inscricao, InputModelInscricao>().ReverseMap();
        }
    }
}
