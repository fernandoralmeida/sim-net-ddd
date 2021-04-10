using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE.Interface
{
    using Domain.SDE.Entity;
    using Application.Interface;
    public interface IAppServiceAmbulante : IAppServiceBase<Ambulante>
    {
        IEnumerable<Ambulante> GetByTitular(string nome);
        IEnumerable<Ambulante> GetByAuxiliar(string nome);
        IEnumerable<Ambulante> GetByAtividade(string atividade);
    }
}
