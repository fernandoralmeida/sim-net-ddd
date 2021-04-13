using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Sim.UI.Web.AutoMapper
{
    using Sim.Domain.SDE.Entity;
    using Web.Pages.Pessoa;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NovoModel.InputModel, Pessoa>();
            CreateMap<Pessoa, NovoModel.InputModel>().ReverseMap();
        }
    }
}
