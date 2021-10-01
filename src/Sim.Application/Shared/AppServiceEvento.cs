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
    public class AppServiceEvento : AppServiceBase<Evento>, IAppServiceEvento
    {
        private readonly IServiceEvento _evento;
        public AppServiceEvento(IServiceEvento evento)
            :base(evento)
        {
            _evento = evento;
        }

        public async Task<IEnumerable<Evento>> EventosAtivos()
        {
            return await _evento.EventosAtivos(ListAll());
        }

        public async Task<IEnumerable<Evento>> EventosPassados()
        {
            return await _evento.EventosPassados(ListAll());
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
