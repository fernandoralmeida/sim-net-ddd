using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Service
{
    using Entity;
    using Interface;
    using Domain.Service;
    public class ServiceDIA : ServiceBase<DIA>, IServiceDIA
    {
        private readonly IRepositoryDIA _dia;
        public ServiceDIA(IRepositoryDIA repositoryDIA):base(repositoryDIA)
        {
            _dia = repositoryDIA;
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
