﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Interface
{
    using Domain.Shared.Entity;
    using Application.Interface;
    public interface IAppServiceTipo : IAppServiceBase<Tipo>
    {
        IEnumerable<Tipo> GetByOwner(string owner);
    }
}