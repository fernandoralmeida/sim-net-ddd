using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Service
{
    using Entity;
    using Domain.Service;
    using Interface;
    public class ServiceInscricao: ServiceBase<Inscricao>, IServiceInscricao
    {
        private readonly IRepositoryInscricao _inscricao;
        public ServiceInscricao(IRepositoryInscricao repositoryInscricao)
            :base(repositoryInscricao)
        {
            _inscricao = repositoryInscricao;
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

        public Task<IEnumerable<Inscricao>> GetInscrito(Guid id)
        {
            return _inscricao.GetInscrito(id);
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
