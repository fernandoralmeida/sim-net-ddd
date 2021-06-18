using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;
    using System.ComponentModel.DataAnnotations;

    [Authorize(Roles = "Administrador,M_Jucesp")]
    public class Jucesp_consulta_razaoModel : PageModel
    {
        private readonly ICNPJBase<BaseJucesp> _empresaApp;

        public Jucesp_consulta_razaoModel(ICNPJBase<BaseJucesp> appServiceEmpresa)
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
            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }

            public IEnumerable<BaseJucesp> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            [Required]
            public string Municipio { get; set; }
        }

        public void OnGet()
        {
            Input = new() { Municipio = "Jau", ListaEmpresas = new List<BaseJucesp>().ToList() };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = Task.Run(() => _empresaApp.ListByRazaoSocialAsync(Input.RazaoSocial, Input.Municipio));

                    await emp;

                    //StatusMessage = "Erro:" + Input.RazaoSocial + "\\n" + Input.Municipio;

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
