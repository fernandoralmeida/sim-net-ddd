using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{

    using Entity;
    using Domain.Interface;
    public interface IRepositoryAmbulante : IRepositoryBase<Ambulante>
    {
        IEnumerable<Ambulante> GetByTitular(string nome);
        IEnumerable<Ambulante> GetByAuxiliar(string nome);
        IEnumerable<Ambulante> GetByAtividade(string atividade);
    }
}
