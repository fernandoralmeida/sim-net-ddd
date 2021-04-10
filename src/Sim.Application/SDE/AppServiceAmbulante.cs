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
    public class AppServiceAmbulante : AppServiceBase<Ambulante>, IAppServiceAmbulante
    {
        private readonly IServiceAmbulante _ambulante;
        public AppServiceAmbulante(IServiceAmbulante ambulante) : base(ambulante)
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
