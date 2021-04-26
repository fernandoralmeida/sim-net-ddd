using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Atendimento
{
    using ViewModel;
    using Sim.Application.Shared.Interface;
    public class NovoModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        public NovoModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
        }

        [BindProperty]
        public VMAtendimento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet()
        {
            Input = new()
            {
                Protocolo = 2000,
                Data = DateTime.Now.Date,
                Status = "ATIVO"
            };
        }

        public async Task<IActionResult> OnPostIncluirPessoa()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostIncluirEmpresa()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostSave()
        {
            return Page();
        }

    }
}
