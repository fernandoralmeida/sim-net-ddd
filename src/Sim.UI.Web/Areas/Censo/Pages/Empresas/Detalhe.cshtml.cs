using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interface;

namespace Sim.UI.Web.Areas.Censo.Pages.Empresas
{
    public class Empresa_detalheModel : PageModel
    {

        private readonly ICNPJBase<BaseReceitaFederal> _empresaApp;

        public Empresa_detalheModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public BaseReceitaFederal Input { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var t = await _empresaApp.GetCnpjAsync(id);

            Input = t;

            return Page();
        }
    }
}
