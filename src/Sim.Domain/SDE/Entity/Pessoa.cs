﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.SDE.Entity
{
    using Shared.Entity;
    public class Pessoa
    {
        public int Pessoa_Id { get; set; }
        // Pessoal
        public string Nome { get; set; }
        public string Nome_Social { get; set; }
        public DateTime? Data_Nascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string RG_Emissor { get; set; }
        public string RG_Emissor_UF { get; set; }
        public string Genero { get; set; }
        public string Deficiencia { get; set; }
        //Correspondencia
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        //Contato
        //public List<KeyValuePair<string, string>> Telefones { get; set; }
        public string Tel_Movel { get; set; }
        public string Tel_Fixo { get; set; }
        public string Email { get; set; }
        //Informacao Cadastro
        public DateTime? Data_Cadastro { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        //Registro relacionais
        //public int Empresa_Id { get; set; }
        //public virtual IEnumerable<Empresa> Empresas { get; set; }
        public virtual ICollection<Atendimento> Atendimento { get; set; }

    }
}
