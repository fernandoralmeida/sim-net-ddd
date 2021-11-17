using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{

    using Sim.Application.SDE.Interface;
    using Sim.Domain.Cnpj.Entity;
    using System.ComponentModel;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appEmpresa;

        public SelectList ListaMunicipios { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<IEnumerable<KeyValuePair<string, int>>> ListEmpresas { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DisplayName("Situação")]
            public string Situacao { get; set; }
            public string Municipio { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceEmpresa appEmpresa)
        {
            _appEmpresa = appEmpresa;
        }

        private async Task LoadMunicipios()
        {

            var t = await _appEmpresa.MicroRegiaoJahu();

            if (t != null)
            {
                ListaMunicipios = new SelectList(t, nameof(Municipio.Codigo), nameof(Municipio.Descricao), null);
            }
        }

        private async Task LoadAsync()
        {
            var l_all_empresas = await _appEmpresa.EmpresasByMunicipioAsync(Input.Municipio, Input.Situacao);
            ListEmpresas.Add(l_all_empresas);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new();
            Input.Municipio = "6607";
            Input.Situacao = "Ativa";
            await LoadMunicipios();
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
