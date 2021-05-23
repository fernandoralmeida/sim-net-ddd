using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Agenda
{
    using Domain.SDE.Entity;
    using Domain.Shared.Entity;


    public class InputModelInscricao
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Numero")]
        public int Numero { get; set; }

        [DisplayName("Setor")]
        public string Owner_Setor { get; set; }

        public Guid AplicationUser_Id { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Time)]
        public DateTime? Data_Inscricao { get; set; }

        public virtual Pessoa Participante { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
