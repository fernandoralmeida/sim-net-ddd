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
        public Guid Id { get; set; }
        public int InscricaoMunicipal { get; set; }
        public string Autorizacao { get; set; }
        public Guid Titular_Pessoa_Id { get; set; }
        public Guid Auxiliar_Pessoa_Id { get; set; }
        public string Atividade { get; set; }
        public string FormaAtuacao { get; set; }
        public string Veiculo { get; set; }
        public DateTime? Emissao { get; set; }
        public DateTime? Validade { get; set; }
        public string Processo { get; set; }
        public string Situacao { get; set; }
        public DateTime? DiaDesde { get; set; }
        public virtual int Contador { get; set; }

        public virtual Pessoa Titular { get; set; }
        public virtual Pessoa Auxiliar { get; set; }
    }

}
