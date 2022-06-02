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

        public async Task<IEnumerable<Evento>> EventosAtivos(int ano)
        {
            return await _evento.EventosAtivos(ListAll().Where(s => s.Data.Value.Year == ano));
        }

        public async Task<IEnumerable<Evento>> EventosFinalizados(int ano)
        {
            return await _evento.EventosFinalizados(ListAll().Where(s => s.Data.Value.Year == ano));
        }

        public async Task<IEnumerable<Evento>> EventosCancelados(int ano)
        {
            return await _evento.EventosCancelados(ListAll().Where(s => s.Data.Value.Year == ano));
        }

        public Evento GetByCodigo(int codigo)
        {
            return _evento.GetByCodigo(codigo);
        }

        public Evento GetByCodigo_Participantes(int codigo)
        {
            return _evento.GetByCodigo_Participantes(codigo);
        }

        public IEnumerable<Evento> GetByNome(string nome, int ano)
        {
            return _evento.GetByNome(nome).Where(s => s.Data.Value.Year == ano);
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

        public Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListarEventosPorMes(IEnumerable<Evento> eventos)
        {
            return _evento.ListarEventosPorMes(eventos);
        }
    }
}
