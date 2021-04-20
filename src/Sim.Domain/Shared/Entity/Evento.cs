﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class Evento
    {
        public Evento()
        {     }
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public string Descricao { get; set; }
        public string Owner { get; set; }
        public int Lotacao { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Inscricao> Inscritos { get; set; }

        public int Inscricoes()
        {
            return 0;
        }
        public int Vagas()
        {
            return 0;
        }
    }
}
