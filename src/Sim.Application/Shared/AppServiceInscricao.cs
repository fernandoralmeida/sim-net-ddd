using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared
{
    using Domain.Shared.Entity;
    using Domain.Shared.Interface;
    using Interface;
    public class AppServiceInscricao : AppServiceBase<Inscricao>, IAppServiceInscricao
    {
        private readonly IServiceInscricao _inscricao;
        public AppServiceInscricao(IServiceInscricao serviceInscricao)
            :base(serviceInscricao)
        {
            _inscricao = serviceInscricao;
        }

        public IEnumerable<Inscricao> GetByEvento(string evento)
        {
            return _inscricao.GetByEvento(evento);
        }

        public IEnumerable<Inscricao> GetByParticipante(string nome)
        {
            return _inscricao.GetByParticipante(nome);
        }

        public IEnumerable<Inscricao> GetByTipo(string evento)
        {
            return _inscricao.GetByTipo(evento);
        }

        public async Task<IEnumerable<Inscricao>> GetInscrito(Guid id)
        {
            return await _inscricao.GetInscrito(id);
        }

        public bool JaInscrito(string cpf, int evento)
        {
            return _inscricao.JaInscrito(cpf, evento);
        }

        public int LastCodigo()
        {
            return _inscricao.LastCodigo();
        }

    }
}
