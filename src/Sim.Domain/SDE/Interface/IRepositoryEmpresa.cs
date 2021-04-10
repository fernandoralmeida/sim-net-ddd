﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IRepositoryEmpresa : IRepositoryBase<Empresa>
    {
        IEnumerable<Empresa> ConsultaByCNPJ(string _cnpj);
        IEnumerable<Empresa> ConsultaByCNAE(string _cnae);
        IEnumerable<Empresa> ConsultaByRazaoSocial(string _name);
    }
}
