﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sim.UI.Web.Pages.Planner
{
    using Domain.Shared.Entity;
    public class InputModelPlanner
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("SEGUNDA")]
        public string Segunda { get; set; }

        [DisplayName("TERÇA")]
        public string Terca { get; set; }

        [DisplayName("QUARTA")]
        public string Quarta { get; set; }

        [DisplayName("QUINTA")]
        public string Quinta { get; set; }

        [DisplayName("SEXTA")]
        public string Sexta { get; set; }

        [DisplayName("SÁBADO")]
        public string Sabado { get; set; }

        [DisplayName("SEMANA QUE VEM")]
        public string ProximaSemana { get; set; }

        [DisplayName("PRIORIDADES")]
        public string Prioridades { get; set; }

        [DisplayName("ANOTAÇÃO")]
        public string Anotacao { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Ultima_Alteracao { get; set; }

        [DisplayName("Operador")]
        public string Owner_AppUser_Id { get; set; }

        [DisplayName("Registro Ativo")]
        public bool Ativo { get; set; }
    }
}
