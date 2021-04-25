using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{
    using Domain.Shared.Entity;
    public class QSA
    {
        public QSA()
        {

        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Qualificacao { get; set; }

        public virtual ICollection<Empresa> Empresa { get; set; }
    }
}
