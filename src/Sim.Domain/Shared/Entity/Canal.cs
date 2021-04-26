using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public  class Canal
    {
        public Canal()
        {

        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Secretaria Secretaria { get; set; } //Secretaria
        public Setor Setor { get; set; } //Setor
        public bool Ativo { get; set; }
    }
}
