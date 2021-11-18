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

        public IEnumerable<CnaeSecao> ListCNAE { get; set; }
    }

    public class CnaeSecao
    {
        public KeyValuePair<string, int> Secao { get; set; }
        public IEnumerable<CnaeDivisao> ListDivisao { get; set; }
    }

    public class CnaeDivisao
    {
        public KeyValuePair<string, int> Divisao { get; set; }
        public IEnumerable<CnaeGrupo> ListGrupo { get; set; }
    }

    public class CnaeGrupo
    {
        public KeyValuePair<string, int> Grupo { get; set; }
        public IEnumerable<CnaeClasse> ListClasse { get; set; }
    }

    public class CnaeClasse
    {
        public KeyValuePair<string, int> Classe { get; set; }
        public IEnumerable<CnaeSubclasse> ListSubclasse { get; set; }
    }

    public class CnaeSubclasse
    {
        public KeyValuePair<string, int> Subclasse { get; set; }
    }
}
