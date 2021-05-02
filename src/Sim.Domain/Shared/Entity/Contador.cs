using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class Contador
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Modulo { get; set; }
        public string AppUserId { get; set; }
        public DateTime? Data { get; set; }
    }
}
