using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class Setor
    {
        public Setor()
        {

        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Secretaria Secretaria { get; set; } //Secretaria
        public bool Ativo { get; set; }

        public virtual ICollection<Canal> Canais { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }
    }
}
