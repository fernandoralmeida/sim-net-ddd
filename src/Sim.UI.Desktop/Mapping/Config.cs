using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Sim.UI.Desktop.Mapping
{
    using Sim.Domain.SDE.Entity;
    using Sim.Service.CNPJ.Entity;
    using System.Linq.Expressions;

    public class Config : Profile
    {
        public Config()
        {
            CreateMap<CNPJ, Empresa>();
            CreateMap<Empresa, CNPJ>().ReverseMap();
            CreateMap<Qsa, QSA>();
            CreateMap<QSA, Qsa>().ReverseMap();
        }
    }
}
