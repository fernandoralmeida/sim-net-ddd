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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BiCnae> ListCnaes { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BaseReceitaFederal> ListCnaeTB { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DisplayName("Situa��o")]
            public string Situacao { get; set; }
            public string Municipio { get; set; }
            public string Mes { get; set; }
            public int Ano { get; set; }
            public string Modo { get; set; }
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

            if (Input.Modo == "Atividades")
            {
                ListCnaes = _appEmpresa.ListBICnae(Input.Municipio).Result;
            }

            else
                ListEmpresas = _appEmpresa.BiEmpresasAsync(Input.Municipio, Input.Situacao, Input.Ano.ToString(), Input.Mes).Result;


        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new();
            Input.Municipio = "6607";
            Input.Situacao = "Ativa";
            Input.Ano = DateTime.Today.Year;
            Input.Mes = "00";
            await LoadMunicipios();            
            //await LoadAsync();            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadAsync();
            return Page();
        }

        /**/
        public JsonResult OnGetPreview(string c, string m)
        {
            var user_atendimentos = _appEmpresa.ListByCNAEAsync(c, m);
            user_atendimentos.Wait();
            return new JsonResult(user_atendimentos.Result);
        }
        
        /*
        public JsonResult OnPostPreview()
        {
            var user_atendimentos = _appEmpresa.BiEmpresasAsync(Input.Municipio, Input.Situacao, Input.Ano.ToString(), Input.Mes);
            user_atendimentos.Wait();
            return new JsonResult(user_atendimentos.Result);
        }
        */
    }
}
