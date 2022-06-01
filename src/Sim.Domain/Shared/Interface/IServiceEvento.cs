using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IServiceEvento : IServiceBase<Evento>
    {
        IEnumerable<Evento> ListAll();
        IEnumerable<Evento> GetByOwner(string setor);
        IEnumerable<Evento> GetByNome(string nome);
        Evento GetByCodigo(int codigo);
        int LastCodigo();
        Task<IEnumerable<Evento>> EventosAtivos(IEnumerable<Evento> eventos);
        Task<IEnumerable<Evento>> EventosPassados(IEnumerable<Evento> eventos);
        Evento GetByCodigo_Participantes(int codigo);
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListarEventosPorMes(IEnumerable<Evento> eventos);
    }
}
