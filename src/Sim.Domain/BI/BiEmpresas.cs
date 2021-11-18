using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.BI
{
    public class BiEmpresas
    {
        public KeyValuePair<string, int> Empresas { get; set; }
        public IEnumerable<KeyValuePair<string, int>> ListaSituacao { get; set; }
        public IEnumerable<KeyValuePair<string, int>> ListaAtividades { get; set; }
        public IEnumerable<KeyValuePair<string, int>> ListaSetores { get; set; }
        public IEnumerable<IEnumerable<KeyValuePair<string, int>>> ListaAtividadesSetores { get; set; }        
    }
}
