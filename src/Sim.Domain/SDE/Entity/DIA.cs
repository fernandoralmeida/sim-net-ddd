using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{
    public class DIA
    {
        public int DIA_Id { get; set; }
        public int InscricaoMunicipal { get; set; }
        public string Autorizacao { get; set; }
        public int Autorizado_Id_Titular { get; set; }
        public virtual AmbulanteAtutorizado Titular { get; set; }
        public int Autorizado_Id_Auxiliar { get; set; }
        public virtual AmbulanteAtutorizado Auxiliar { get; set; }
        public string Atividade { get; set; }
        public string FormaAtuacao { get; set; }
        public int Veiculo_Id { get; set; }
        public virtual AmbulanteVeiculo Veiculo { get; set; }
        public DateTime? Emissao { get; set; }
        public DateTime? Validade { get; set; }
        public string Processo { get; set; }
        public string Situacao { get; set; }
        public DateTime? DiaDesde { get; set; }
        public virtual int Contador { get; set; }
    }

    public class AmbulanteAtutorizado
    {
        public int Autorizado_Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Contato { get; set; }
        public virtual ICollection<DIA> DIAs { get; set; }
        public virtual ICollection<Ambulante> Ambulantes { get; set; }
    }

    public class AmbulanteVeiculo
    {
        public int Veiculo_Id { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Placa { get; set; }
        public virtual ICollection<DIA> DIAs { get; set; }
        public virtual ICollection<Ambulante> Ambulantes { get; set; }
    }
}
