using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{

    using Sim.Application.SDE.Interface;
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.BI;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appEmpresa;

        public SelectList ListaMunicipios { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BiEmpresas> ListEmpresas { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DisplayName("Situa��o")]
            public string Situacao { get; set; }
            public string Municipio { get; set; }
            public string Mes { get; set; }
            public int Ano { get; set; }
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
            await LoadMunicipios();
            ListEmpresas = _appEmpresa.BiEmpresasAsync(Input.Municipio, Input.Situacao,Input.Ano.ToString(), Input.Mes).Result;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new();
            Input.Municipio = "6607";
            Input.Situacao = "Ativa";
            Input.Ano = DateTime.Today.Year;
            Input.Mes = DateTime.Today.Month.ToString();
            await LoadMunicipios();
            //await LoadAsync();            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadAsync();
            return Page();
        }

        public JsonResult OnGetViewChart()
        {

            return new JsonResult(null);
        }

        public JsonResult OnGetPreview(string id)
        {
            var user_atendimentos = Task.Run(() => _appEmpresa.List());
            user_atendimentos.Wait();
            return new JsonResult(user_atendimentos.Result);
        }
    }
}
