using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sim.UI.Web.Areas.Settings.ViewModel.Common
{
    using Sim.Domain.Shared.Entity;
    public class VMEvento
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("Tipo")]
        public string Tipo { get; set; } //Tipo

        [DisplayName("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Evento> Listar { get; set; }
    }
}
