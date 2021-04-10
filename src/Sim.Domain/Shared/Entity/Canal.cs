using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public  class Canal
    {
        public int Canal_Id { get; set; }
        public string Nome { get; set; }
        public string Owner { get; set; }
        public bool Ativo { get; set; }
    }
}
