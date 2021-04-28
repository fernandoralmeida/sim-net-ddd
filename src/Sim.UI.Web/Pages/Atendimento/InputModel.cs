using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Domain.SDE.Entity;

    public class InputModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Protocolo")]
        public string Protocolo { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }

        [DisplayName("Inicio")]
        [DataType(DataType.Time)]
        public DateTime? Inicio { get; set; }
        
        [DisplayName("Fim")]
        [DataType(DataType.Time)]
        public DateTime? Fim { get; set; }

        [DisplayName("Setor")]
        public string Setor { get; set; }

        [DisplayName("Canal do Atendimento")]
        public string Canal { get; set; }

        [DisplayName("Serviços")]
        public string Servicos { get; set; }

        [DisplayName("Descrição do Atendimento")]
        public string Descricao { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public Guid Owner_AppUser_Id { get; set; }

        [DisplayName("Pessoa")]
        public virtual Pessoa Pessoa { get; set; }

        [DisplayName("Empresa")]
        public virtual Empresa Empresa { get; set; }
    }
}
