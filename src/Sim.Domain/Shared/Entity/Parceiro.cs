using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class Parceiro
    {
        public Parceiro()
        {

        }

        public Guid Id { get; set; }
        public string Nome { get; set; } //nome do tipo
        public Secretaria Secretaria { get; set; } //Tipo
        public bool Ativo { get; set; }
    }
}
