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
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;
    using System.ComponentModel.DataAnnotations;

    [Authorize]
    public class Consulta_rsModel : PageModel
    {
        private readonly IAppServiceEmpresa  _empresaApp;

        public Consulta_rsModel(IAppServiceEmpresa appServiceEmpresa)
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

            public IEnumerable<Empresa> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            public string CNPJRes { get; set; }
        }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => _empresaApp.List());

            await t;

            Input = new InputModel
            {
                ListaEmpresas = t.Result
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = Task.Run(() => _empresaApp.ConsultaByRazaoSocial(Input.RazaoSocial));

                    await emp;

                    Input = new InputModel
                    {
                        ListaEmpresas = emp.Result,
                        CNPJRes = new Functions.Mask().Remove(Input.CNPJ)

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
