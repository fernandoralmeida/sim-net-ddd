using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{
    using Domain.Shared.Entity;

    public class Empresa
    {
        public int Empresa_Id { get; set; }
        public string CNPJ { get; set; }
        public string Tipo { get; set; }
        public DateTime? Data_Abertura { get; set; }
        public string Nome_Empresarial { get; set; }
        public string Nome_Fantasia { get; set; }
        public string Porte { get; set; }
        public string CNAE_Principal { get; set; }
        public string Atividade_Principal { get; set; }
        public string Atividade_Secundarias { get; set; }
        public string Natureza_Juridica { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Ente_Federativo_Resp { get; set; }
        public string Situacao_Cadastral { get; set; }
        public DateTime? Data_Situacao_Cadastral { get; set; }
        public string Motivo_Situacao_Cadastral { get; set; }
        public string Situacao_Especial { get; set; }
        public DateTime? Data_Situacao_Especial { get; set; }
        public decimal Capital_Social { get; set; }

        //Registros relacionais
        public virtual ICollection<QSA> QSA { get; set; }
        public virtual ICollection<Atendimento> Atendimento { get; set; }

    }
}
