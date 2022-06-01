using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Interface
{
    using Domain.Shared.Entity;
    using Application.Interface;
    public interface IAppServiceEvento : IAppServiceBase<Evento>
    {
        IEnumerable<Evento> ListAll();
        IEnumerable<Evento> GetByOwner(string setor);
        IEnumerable<Evento> GetByNome(string nome, int ano);
        Evento GetByCodigo(int codigo);
        int LastCodigo();
        Task<IEnumerable<Evento>> EventosAtivos(int ano);
        Task<IEnumerable<Evento>> EventosPassados(int ano);
        Evento GetByCodigo_Participantes(int codigo);
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListarEventosPorMes(IEnumerable<Evento> eventos);

    }
}
