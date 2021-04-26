using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sim.UI.Web.ViewModel
{
    using Sim.Domain.SDE.Entity;


    public class VMAtendimento
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }
        [DisplayName("Nome Empresarial")]
        public int Protocolo { get; set; }
        [DisplayName("Nome Empresarial")]
        public DateTime? Data { get; set; }
        [DisplayName("Nome Empresarial")]
        public DateTime? Inicio { get; set; }
        [DisplayName("Nome Empresarial")]
        public DateTime? Fim { get; set; }
        [DisplayName("Nome Empresarial")]
        public string Setor { get; set; }
        [DisplayName("Nome Empresarial")]
        public string Canal { get; set; }
        [DisplayName("Nome Empresarial")]
        public string Servicos { get; set; }
        [DisplayName("Nome Empresarial")]
        public string Descricao { get; set; }
        [DisplayName("Nome Empresarial")]
        public string Status { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public Guid Owner_AppUser_Id { get; set; }

        [DisplayName("Nome Empresarial")]
        public virtual Pessoa Pessoa { get; set; }

        [DisplayName("Nome Empresarial")]
        public virtual Empresa Empresa { get; set; }
    }
}
