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
    public class ServiceEvento : ServiceBase<Evento>, IServiceEvento
    {
        private readonly IRepositoryEvento _evento;
        public ServiceEvento(IRepositoryEvento repositoryEvento)
            :base(repositoryEvento)
        {
            _evento = repositoryEvento;
        }

        public Evento GetByCodigo(int codigo)
        {
            return _evento.GetByCodigo(codigo);
        }

        public IEnumerable<Evento> GetByNome(string nome)
        {
            return _evento.GetByNome(nome);
        }

        public IEnumerable<Evento> GetByOwner(string setor)
        {
            return _evento.GetByOwner(setor);
        }

        public int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        public IEnumerable<Evento> ListAll()
        {
            return _evento.ListAll();
        }
    }
}
