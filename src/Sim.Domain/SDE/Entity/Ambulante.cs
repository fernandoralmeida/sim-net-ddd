using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{   
    public class Ambulante
    {
        public int Ambulante_Id { get; set; }
        public string Protocolo { get; set; }
        public int Autorizado_Id_Titular { get; set; }
        public virtual AmbulanteAtutorizado Titular { get; set; }
        public int Autorizado_Id_Auxiliar { get; set; }
        public virtual AmbulanteAtutorizado Auxiliar { get; set; }
        public string FormaAtuacao { get; set; }
        public string Local { get; set; }
        public string Atividade { get; set; }
        public DateTime? Data_Cadastro { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public virtual int Contador { get; set; }
    }
}
