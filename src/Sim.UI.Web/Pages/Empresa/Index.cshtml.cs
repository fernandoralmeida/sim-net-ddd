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
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList Municipios { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Required]
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

            Input = new InputModel
            {
                ListaEmpresas = await _empresaApp.ListTop20()
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadMunicipios();
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = Task.Run(() => _empresaApp.ConsultaByCNPJ(Input.CNPJ));

                    await emp;

                    Input = new InputModel
                    {
                        ListaEmpresas = emp.Result,
                        CNPJRes = Input.CNPJ.MaskRemove()

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
