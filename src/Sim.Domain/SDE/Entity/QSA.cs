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
        public int QSA_Id { get; set; }
        public string Nome { get; set; }
        public string Qualificacao { get; set; }
        public int Empresa_Id { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
