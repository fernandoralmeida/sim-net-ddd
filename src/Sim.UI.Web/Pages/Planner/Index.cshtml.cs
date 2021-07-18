using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Planner
{
    [Authorize(Roles = "Administrador")]
    public class IndexModel : PageModel
    {
        public IndexModel() { }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelPlanner Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? InicioSemana { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FimSemana { get; set; }

        public void OnGet()
        {
            PlannerTimer(DateTime.Now);
        }

        private void PlannerTimer(DateTime dia)
        {
            var dia_da_semana = dia.DayOfWeek;
            InicioSemana = dia;
            FimSemana = dia;

            switch (dia_da_semana)
            {
                case DayOfWeek.Sunday:
                    InicioSemana = dia.AddDays(0);
                    FimSemana = dia.AddDays(6);
                    break;

                case DayOfWeek.Monday:
                    InicioSemana = dia.AddDays(-1);
                    FimSemana = dia.AddDays(5);
                    break;

                case DayOfWeek.Tuesday:
                    InicioSemana = dia.AddDays(-2);
                    FimSemana = dia.AddDays(4);
                    break;

                case DayOfWeek.Wednesday:
                    InicioSemana = dia.AddDays(-3);
                    FimSemana = dia.AddDays(3);
                    break;

                case DayOfWeek.Thursday:
                    InicioSemana = dia.AddDays(-4);
                    FimSemana = dia.AddDays(2);
                    break;

                case DayOfWeek.Friday:
                    InicioSemana = dia.AddDays(-5);
                    FimSemana = dia.AddDays(1);
                    break;

                case DayOfWeek.Saturday:
                    InicioSemana = dia.AddDays(-6);
                    FimSemana = dia.AddDays(0);
                    break;

                default:
                    break;
            }

        }
    }
}
