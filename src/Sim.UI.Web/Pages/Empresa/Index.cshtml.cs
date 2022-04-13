using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Cnpj.Entity;
    using Functions;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa  _empresaApp;

        public IndexModel(IAppServiceEmpresa appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
            Input = new();
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList Municipios { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public ParamModel GetParam { get; set; }

        public class InputModel
        {
            
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }

            [DisplayName("Data Inicial")]
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DisplayName("Data Final")]
            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string Base { get; set; }

            public string CNAE { get; set; }

            public string Situacao { get; set; }

            public string Logradouro { get; set; }

            public string Bairro { get; set; }

            public string Municipio { get; set; }

            public string Socio { get; set; }

            [DisplayName("Regime Tributário")]
            public string RegimeTributario { get; set; }

            public IEnumerable<Empresas> ListaEmpresas { get; set; }

            public ICollection<BaseReceitaFederal> ListaEmpresasRFB { get; set; }
            public string CNPJRes { get; set; }
        }

        public class ParamModel
        {
            public string param1 { get; set; }
            public string param2 { get; set; }
            public string param3 { get; set; }
            public string param4 { get; set; }
            public string param5 { get; set; }
            public string param6 { get; set; }
            public string param7 { get; set; }
            public string param8 { get; set; }
            public string param9 { get; set; }
        }

        private async Task LoadMunicipios()
        {
            var t = Task.Run(() => _empresaApp.MicroRegiaoJahu());
            await t;

            if (t != null)
            {
                Municipios = new SelectList(t.Result, nameof(Municipio.Codigo), nameof(Municipio.Descricao), null);
            }
        }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => { });

            await t;

            Input.ListaEmpresasRFB = new List<BaseReceitaFederal>();// await _empresaApp.ListTop20();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadMunicipios();
            await LoadAsync();
            Input.Municipio = "6607";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await LoadMunicipios();

                    var param = new List<object>() {
                    Input.Base,
                    Input.CNPJ.MaskRemove(),
                    Input.RazaoSocial,
                    Input.CNAE.MaskRemove(),
                    Input.Situacao,
                    Input.Logradouro,
                    Input.Bairro,
                    Input.Socio,
                    Input.Municipio};

                    var lista = await _empresaApp.ListByParam(param);

                    Input.ListaEmpresasRFB = lista.ToList();

                    GetParam.param1 = (string)param[0];
                    GetParam.param2 = (string)param[1] != null ? (string)param[1] : "0";
                    GetParam.param3 = (string)param[2] != null ? (string)param[2] : "0";
                    GetParam.param4 = (string)param[3] != null ? (string)param[3] : "0";
                    GetParam.param5 = (string)param[4] != null ? (string)param[4] : "0";
                    GetParam.param6 = (string)param[5] != null ? (string)param[5] : "0";
                    GetParam.param7 = (string)param[6] != null ? (string)param[6] : "0";
                    GetParam.param8 = (string)param[7] != null ? (string)param[7] : "0";
                    GetParam.param9 = (string)param[8] != null ? (string)param[8] : "0";

                    Input.CNPJRes = Input.CNPJ.MaskRemove();
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
