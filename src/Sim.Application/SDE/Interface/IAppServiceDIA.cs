using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE.Interface
{
    using Domain.SDE.Entity;
    using Application.Interface;
    public interface IAppServiceDIA : IAppServiceBase<DIA>
    {
        IEnumerable<DIA> GetByTitular(string nome);
        IEnumerable<DIA> GetByAuxiliar(string nome);
        IEnumerable<DIA> GetByAtividade(string atividade);
        IEnumerable<DIA> GetVencidos();
        IEnumerable<DIA> GetAtivos();
        IEnumerable<DIA> GetBaixados();        
    }
}
