using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class StatusAtendimento
    {
        public Guid Id { get; set; }
        public string UnserName { get; set; }
        public bool Online { get; set; }
    }
}
