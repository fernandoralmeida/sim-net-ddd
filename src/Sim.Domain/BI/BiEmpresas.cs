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

        public IEnumerable<ConclaSecao> ListCNAE { get; set; }
    }

    public class ConclaSecao
    {
        public KeyValuePair<string, int> Secao { get; set; }
        public IEnumerable<ConclaDivisao> ListDivisao { get; set; }
    }

    public class ConclaDivisao
    {
        public KeyValuePair<string, int> Divisao { get; set; }
        public IEnumerable<ConclaGrupo> ListGrupo { get; set; }
    }

    public class ConclaGrupo
    {
        public KeyValuePair<string, int> Grupo { get; set; }
        public IEnumerable<ConclaClasse> ListClasse { get; set; }
    }

    public class ConclaClasse
    {
        public KeyValuePair<string, int> Classe { get; set; }
        public IEnumerable<ConclaSubclasse> ListSubclasse { get; set; }
    }

    public class ConclaSubclasse
    {
        public KeyValuePair<string, int> Subclasse { get; set; }
    }
}
