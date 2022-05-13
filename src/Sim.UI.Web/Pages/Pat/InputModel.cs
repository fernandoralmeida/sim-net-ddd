using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Pat
{
    using Sim.Domain.SDE.Entity;
    public class InputModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Ocupação exigida!")]
        [DisplayName("Ocupação")]
        public string Ocupacao { get; set; }

        [Required(ErrorMessage = "Experiência exigida!")]
        [DisplayName("Experiência")]
        public string Experiencia { get; set; }

        [DisplayName("Salário Médio")]
        public string Salario { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de vagas!")]
        [DisplayName("Vagas Disponível")]
        public string Vagas { get; set; }

        [DisplayName("Empresa")]
        public Empresas Empresa { get; set; }
    }
}
