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
    public class VMServico
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Secretaria")]
        public Secretaria Secretaria { get; set; } //Secretaria

        [DisplayName("Setor")]
        public Setor Setor { get; set; } //Setor

        [DisplayName("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Servico> Listar { get; set; }
    }
}
