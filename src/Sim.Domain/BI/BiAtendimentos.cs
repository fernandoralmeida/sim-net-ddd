using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.BI
{
    public class BiAtendimentos
    {
        public KeyValuePair<string, int> Antendimentos_Ano { get; set; }
        public KeyValuePair<string, int> Servicos_Ano { get; set; }
        public KeyValuePair<string, int> Pessoas_Ano { get; set; }
        public KeyValuePair<string, int> Pessoas_Servicos_Ano { get; set; }
        public KeyValuePair<string, int> Empresas_Ano { get; set; }
        public KeyValuePair<string, int> Empresas_Servicos_Ano { get; set; }
        public IEnumerable<BiAtendimentoMeses> Meses { get; set; }

        public class BiAtendimentoMeses
        {
            public KeyValuePair<string, int> Mes { get; set; }
            public IEnumerable<BiSetores> Setores { get; set; }
        }

        public class BiSetores
        {
            public KeyValuePair<string, int> Setor { get; set; }
            public IEnumerable<KeyValuePair<string, int>> Detalhes { get; set; }
        }
    }
}
