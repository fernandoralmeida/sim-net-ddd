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
        IEnumerable<Evento> GetByNome(string nome);
        Evento GetByCodigo(int codigo);
        int LastCodigo();
        Task<IEnumerable<Evento>> EventosAtivos();
        Task<IEnumerable<Evento>> EventosPassados();

        Evento GetByCodigo_Participantes(int codigo);
    }
}
