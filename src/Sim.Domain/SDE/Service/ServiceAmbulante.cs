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
    public class ServiceAmbulante : ServiceBase<Ambulante>, IServiceAmbulante
    {
        private readonly IRepositoryAmbulante _ambulante;
        public ServiceAmbulante(IRepositoryAmbulante ambulante) : base(ambulante)
        {
            _ambulante = ambulante;
        }

        public IEnumerable<Ambulante> GetByAtividade(string atividade)
        {
            return _ambulante.GetByAtividade(atividade);
        }

        public IEnumerable<Ambulante> GetByAuxiliar(string nome)
        {
            return _ambulante.GetByAuxiliar(nome);
        }

        public IEnumerable<Ambulante> GetByTitular(string nome)
        {
            return _ambulante.GetByTitular(nome);
        }
    }
}
