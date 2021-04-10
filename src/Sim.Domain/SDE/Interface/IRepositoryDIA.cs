using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IRepositoryDIA : IRepositoryBase<DIA>
    {
        IEnumerable<DIA> GetByTitular(string nome);
        IEnumerable<DIA> GetByAuxiliar(string nome);
        IEnumerable<DIA> GetByAtividade(string atividade);
        IEnumerable<DIA> GetVencidos();
        IEnumerable<DIA> GetAtivos();
        IEnumerable<DIA> GetBaixados();
    }
}
