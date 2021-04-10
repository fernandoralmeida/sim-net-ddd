using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class Evento
    {
        public int Evento_Id { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public string Descricao { get; set; }
        public string Owner { get; set; }
        public bool Ativo { get; set; }
        public virtual int Inscritos { get; set; }
    }
}
