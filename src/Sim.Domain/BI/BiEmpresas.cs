using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.BI
{
    public class BiEmpresas
    {
        public KeyValuePair<string, int> TotalEmpresas { get; set; }
        public KeyValuePair<string, int> EmpresasAtivas { get; set; }
        public IEnumerable<KeyValuePair<string, int>> ListaSituacao { get; set; }
        public IEnumerable<KeyValuePair<string, int>> ListaAtividades { get; set; }
        public IEnumerable<KeyValuePair<string, int>> ListaSetores { get; set; }
        public IEnumerable<IEnumerable<KeyValuePair<string, int>>> ListaAtividadesSetores { get; set; }

        public IEnumerable<KeyValuePair<string, int>> Servicos { get; set; }
        public IEnumerable<KeyValuePair<string, int>> Comercio { get; set; }
        public IEnumerable<KeyValuePair<string, int>> Indistria { get; set; }
        public IEnumerable<KeyValuePair<string, int>> Agro { get; set; }
        public IEnumerable<KeyValuePair<string, int>> Construcao { get; set; }

        public KeyValuePair<string, int> Formalizacoes { get; set; }
        public KeyValuePair<string, int> Baixas { get; set; }

        public KeyValuePair<string, int> SimplesNacional { get; set; }
        public KeyValuePair<string, int> OptanteMEI { get; set; }
        public IEnumerable< KeyValuePair<string, int>> Porte { get; set; }

    }
}


