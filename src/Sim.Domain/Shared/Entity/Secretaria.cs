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
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Owner { get; set; } //Prefeitura
        public bool Ativo { get; set; }
    }
}
