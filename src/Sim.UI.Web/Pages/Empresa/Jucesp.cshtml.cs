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
    public class JucespModel : PageModel
    {
        private readonly ICNPJBase<BaseJucesp> _empresaApp;

        public JucespModel(ICNPJBase<BaseJucesp> appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Required]
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }

            public IEnumerable<BaseJucesp> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            public string CNPJRes { get; set; }
        }

        private async Task LoadAsync()
        {
            var t = await _empresaApp.ListTop10();

            Input = new InputModel
            {
                ListaEmpresas = t
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
                    var emp = await _empresaApp.ListByCnpjAsync(Input.CNPJ);

                    Input = new InputModel
                    {
                        ListaEmpresas = emp,
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
