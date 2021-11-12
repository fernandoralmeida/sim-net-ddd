using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Shared.Entity;
    using System;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appEmpresa;

        [BindProperty(SupportsGet = true)]
        public List<IEnumerable<KeyValuePair<string, int>>> Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceEmpresa appEmpresa)
        {
            _appEmpresa = appEmpresa;
        }

        private async Task LoadAsync()
        {
            var l_all_atendimentos = Task.Run(()=>  new List<KeyValuePair<string, int>>());

            await l_all_atendimentos;

            Input.Add(l_all_atendimentos.Result);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public JsonResult OnGetPreview(string id)
        {
            var user_atendimentos = Task.Run(() => _appEmpresa.List());
            user_atendimentos.Wait();
            return new JsonResult(user_atendimentos.Result);
        }
    }
}
