using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.BI
{
    public class BiCnae
    {
        public List<CnaeSecao> ListaSecao { get; set; }
    }

    public class CnaeSecao
    {
        public KeyValuePair<string, int> Secao { get; set; }
        public List<CnaeClasse> ListaClasse { get; set; }
    }

    public class CnaeClasse
    {
        public KeyValuePair<string, int> Classe { get; set; }
        public List<KeyValuePair<string, int>> ListaSubClasse { get; set; }
    }
}
