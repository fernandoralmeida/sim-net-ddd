﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Interface
{
    using Entity;
    using Domain.Interface;
    public interface IRepositoryInscricao : IRepositoryBase<Inscricao>
    {
        IEnumerable<Inscricao> GetByEvento(string evento);

        IEnumerable<Inscricao> GetByParticipante(string nome);

        IEnumerable<Inscricao> GetByTipo(string evento);

        bool JaInscrito(string cpf, int evento);

        int LastCodigo();
    }
}
