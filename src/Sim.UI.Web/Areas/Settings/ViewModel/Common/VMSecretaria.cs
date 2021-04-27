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
    public class VMSecretaria
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Unidade Responsável")]
        public string Owner { get; set; } //Prefeitura

        [DisplayName("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Secretaria> Listar { get; set; }
    }
}
