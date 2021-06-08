using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    using SDE.Entity;
    public class Inscricao
    {
        public Inscricao()
        {

        }
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public string AplicationUser_Id { get; set; }
        public DateTime? Data_Inscricao { get; set; }
        public bool Presente { get; set; }

        public virtual Pessoa Participante { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
