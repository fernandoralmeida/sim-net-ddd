using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    public class Inscricao
    {
        public Inscricao()
        {

        }
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public Guid Evento_Id { get; set; }
        public Guid Pessoa_Id { get; set; }
        public Guid Empresa_Id { get; set; }
        public string Owner_Setor { get; set; }
        public Guid Owner_AppUser_Id { get; set; }
        public DateTime? Data_Inscricao { get; set; }
    }
}
