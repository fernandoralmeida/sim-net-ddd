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

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NovoModel.InputModel, Pessoa>();
            CreateMap<Pessoa, NovoModel.InputModel>().ReverseMap();

            CreateMap<EditModel.InputModel, Pessoa>();
            CreateMap<Pessoa, EditModel.InputModel>().ReverseMap();

            CreateMap<VMEmpresa, CNPJ>();
            CreateMap<CNPJ, VMEmpresa>().ReverseMap();

            CreateMap<VMEmpresa, Empresa>();
            CreateMap<Empresa, VMEmpresa>().ReverseMap();

            CreateMap<Pages.Atendimento.InputModel, Atendimento>();
            CreateMap<Atendimento, Pages.Atendimento.InputModel>().ReverseMap();
        }
    }
}
