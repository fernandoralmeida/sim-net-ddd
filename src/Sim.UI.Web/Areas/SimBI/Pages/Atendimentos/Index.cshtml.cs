using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Areas.SimBI.Pages.Atendimentos
{

    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;
    using System;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appAtendimento;
        private readonly IAppServiceSetor _appSetores;

        [BindProperty(SupportsGet = true)]
        public List<IEnumerable<KeyValuePair<string, int>>> Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceAtendimento appAtendimento,
            IAppServiceSetor appsetores)
        {
            _appAtendimento = appAtendimento;
            _appSetores = appsetores;
        }

        private async Task LoadAsync()
        {
            var l_all_atendimentos = await _appAtendimento.ByAll();

            Input.Add(l_all_atendimentos);

            var setores = _appSetores.List();

            foreach (Setor s in setores)
            {
                var setor = await _appAtendimento.BySetor(s.Nome);
                if (setor.Count() > 1)
                    Input.Add(setor);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public JsonResult OnGetPreview(string id)
        {
            var user_atendimentos = Task.Run(() => _appAtendimento.ByUserName(id));
            user_atendimentos.Wait();
            return new JsonResult(user_atendimentos.Result);
        }
    }
}
