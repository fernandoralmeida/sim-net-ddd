using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;

    using Sim.Application.Interface;


    [Authorize]
    public class Jucesp_consulta_logradouroModel : PageModel
    {
        private readonly ICNPJBase<BaseJucesp> _empresaApp;

        public Jucesp_consulta_logradouroModel(ICNPJBase<BaseJucesp> appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [Required]
            [DisplayName("Logradouro")]
            public string Logradouro { get; set; }

            public IEnumerable<BaseJucesp> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            [Required]
            public string Municipio { get; set; }
        }

        public void OnGet()
        {
            Input = new() { Municipio = "Jau" };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = Task.Run(() => _empresaApp.ListByLogradouroAsync(Input.Logradouro, Input.Municipio));

                    await emp;

                    Input = new InputModel
                    {
                        ListaEmpresas = emp.Result,
                    };
                }

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }
    }
}
