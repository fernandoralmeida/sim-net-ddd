using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interface;

namespace Sim.UI.Web.Pages.Empresa
{
    public class Jucesp_detailModel : PageModel
    {
        private readonly ICNPJBase<BaseJucesp> _empresaApp;

        public Jucesp_detailModel(ICNPJBase<BaseJucesp> appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public BaseJucesp Input { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var t = await _empresaApp.GetCnpjAsync(id);

            Input = t;

            return Page();
        }
    }
}
