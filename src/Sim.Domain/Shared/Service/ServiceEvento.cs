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

        public async Task<IEnumerable<Evento>> EventosAtivos(IEnumerable<Evento> eventos)
        {
            var t = Task.Run(() => eventos.Where(s => s.EventosAtivos(s)).OrderBy(o => o.Data));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<Evento>> EventosPassados(IEnumerable<Evento> eventos)
        {
            var t = Task.Run(() => eventos.Where(s => s.EventosPassados(s)).OrderByDescending(o => o.Data));

            await t;

            return t.Result;
        }

        public Evento GetByCodigo(int codigo)
        {
            return _evento.GetByCodigo(codigo);
        }

        public Evento GetByCodigo_Participantes(int codigo)
        {
            return _evento.GetByCodigo_Participantes(codigo);
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
