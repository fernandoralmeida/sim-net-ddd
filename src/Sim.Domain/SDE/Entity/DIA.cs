using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{
    public class DIA
    {
        public DIA()
        {

        }
        public int Id { get; set; }
        public int InscricaoMunicipal { get; set; }
        public string Autorizacao { get; set; }
        public string Titular { get; set; }
        public string Auxiliar { get; set; }
        public string Atividade { get; set; }
        public string FormaAtuacao { get; set; }
        public string Veiculo { get; set; }
        public DateTime? Emissao { get; set; }
        public DateTime? Validade { get; set; }
        public string Processo { get; set; }
        public string Situacao { get; set; }
        public DateTime? DiaDesde { get; set; }
        public virtual int Contador { get; set; }
    }

}
