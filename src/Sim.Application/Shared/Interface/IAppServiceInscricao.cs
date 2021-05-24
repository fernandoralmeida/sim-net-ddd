using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Shared.Interface
{
    using Domain.Shared.Entity;
    using Application.Interface;
    public interface IAppServiceInscricao : IAppServiceBase<Inscricao>
    {
        IEnumerable<Inscricao> GetByEvento(string evento);
        IEnumerable<Inscricao> GetByParticipante(string nome);
        IEnumerable<Inscricao> GetByTipo(string evento);
        bool JaInscrito(string cpf, int evento);
        int LastCodigo();
    }
}
