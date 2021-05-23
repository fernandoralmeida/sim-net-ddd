﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Sim.UI.Web.Pages.Agenda
{
    public class InputModelEvento
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Código")]
        public int Codigo { get; set; }

        [Required]
        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("Formato")]
        public string Formato { get; set; }

        [Required]
        [DisplayName("Data")]
        [DataType(DataType.DateTime)]
        public DateTime? Data { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required]
        [DisplayName("Setor Responsável")]
        public string Owner { get; set; }

        [DisplayName("Lotação")]
        public int Lotacao { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<InputModelInscricao> Inscritos { get; set; }
    }
}
