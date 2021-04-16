﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Sim.UI.Web.AutoMapper
{
    using Sim.Domain.SDE.Entity;
    using Web.Pages.Pessoa;
    using Web.Pages.Empresa;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NovoModel.InputModel, Pessoa>();
            CreateMap<Pessoa, NovoModel.InputModel>().ReverseMap();

            CreateMap<EditModel.InputModel, Pessoa>();
            CreateMap<Pessoa, EditModel.InputModel>().ReverseMap();

            CreateMap<VMEmpresa, Empresa>();
            CreateMap<Empresa, VMEmpresa>().ReverseMap();
        }
    }
}