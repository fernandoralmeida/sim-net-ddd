﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{
    using Domain.Shared.Entity;
    using System.ComponentModel.DataAnnotations;

    public class Empresas
    {
        public Empresas()
        {

        }
        public Guid Id { get; set; }
        public string CNPJ { get; set; }
        public DateTime? Data_Abertura { get; set; }
        public string Nome_Empresarial { get; set; }
        public string Nome_Fantasia { get; set; }
        public string CNAE_Principal { get; set; }
        public string Atividade_Principal { get; set; }
        public string Atividade_Secundarias { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Situacao_Cadastral { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }
        public virtual ICollection<Inscricao> Inscricoes { get; set; }

        public bool UltimasFormalizacoes(Empresas obj)
        {
            return obj.Data_Abertura >= DateTime.Now.AddDays(-60);
        }
    }
}
