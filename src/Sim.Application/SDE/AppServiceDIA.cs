using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.SDE
{
    using Interface;
    using Domain.SDE.Entity;
    using Domain.SDE.Interface;
    public class AppServiceDIA : AppServiceBase<DIA>, IAppServiceDIA
    {
        private readonly IServiceDIA _dia;
        public AppServiceDIA(IServiceDIA serviceDIA):base(serviceDIA)
        {
            _dia = serviceDIA;
        }
        public IEnumerable<DIA> GetAtivos()
        {
            return _dia.GetAtivos();
        }

        public IEnumerable<DIA> GetBaixados()
        {
            return _dia.GetBaixados();
        }

        public IEnumerable<DIA> GetByAtividade(string atividade)
        {
            return _dia.GetByAtividade(atividade);
        }

        public IEnumerable<DIA> GetByAuxiliar(string nome)
        {
            return _dia.GetByAuxiliar(nome);
        }

        public IEnumerable<DIA> GetByTitular(string nome)
        {
            return _dia.GetByTitular(nome);
        }

        public IEnumerable<DIA> GetVencidos()
        {
            return _dia.GetVencidos();
        }
    }
}
