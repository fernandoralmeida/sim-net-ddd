using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class Secretaria
    {
        public Secretaria()
        {

        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Owner { get; set; } //Prefeitura
        public bool Ativo { get; set; }

        public virtual ICollection<Canal> Canais { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }
        public virtual ICollection<Setor> Setores { get; set; }
    }
}
