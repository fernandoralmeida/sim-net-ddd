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
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public int Evento_Id { get; set; }
        public int Pessoa_Id { get; set; }
        public int Empresa_Id { get; set; }
        public DateTime? Data_Inscricao { get; set; }
    }
}
