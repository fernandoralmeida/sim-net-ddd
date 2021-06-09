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
               
        [DisplayName("Fim")]
        [DataType(DataType.Time)]
        public DateTime? DataF { get; set; }

        [Required(ErrorMessage = "Selecione o setor do atendimento!")]
        [DisplayName("Setor")]
        public string Setor { get; set; }

        [Required(ErrorMessage = "Selecione o canal do atendimento!")]
        [DisplayName("Canal do Atendimento")]
        public string Canal { get; set; }

        [Required(ErrorMessage = "Adicione um serviço ou mais!")]
        [DisplayName("Serviços")]
        public string Servicos { get; set; }
        
        //[Required]
        [DisplayName("Descrição do Atendimento")]
        public string Descricao { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public string Owner_AppUser_Id { get; set; }

        [DisplayName("Pessoa")]
        public virtual Pessoa Pessoa { get; set; }

        [DisplayName("Empresa")]
        public virtual Empresa Empresa { get; set; }

        [DisplayName("N.o RAE")]
        public virtual RaeSebrae Sebrae { get; set; }
    }
}
