using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Sim.UI.Web.Areas.Censo.Pages.Empresas
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;


    [Authorize(Roles = "Administrador,M_RFB")]
    public class Consulta_socioModel : PageModel
    {
        private readonly ICNPJBase<BaseReceitaFederal> _empresaApp;
        private readonly IServiceMunicipios<Municipio> _municipios;
        public Consulta_socioModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa,
            IServiceMunicipios<Municipio> municipios)
        {
            _empresaApp = appServiceEmpresa;
            _municipios = municipios;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList ListaMunicipios { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("Sócio")]
            public string Socios { get; set; }

            public IEnumerable<BaseReceitaFederal> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            public string Municipio { get; set; }
        }

        private async Task LoadMunicipios()
        {
            var t = await _municipios.MicroRegiaoJahu();

            if (t != null)
            {
                ListaMunicipios = new SelectList(t, nameof(Municipio.Codigo), nameof(Municipio.Descricao), null);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadMunicipios();

            Input = new InputModel
            {
                Municipio = "6607",
                ListaEmpresas = new List<BaseReceitaFederal>().ToList()
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await LoadMunicipios();

                    var emp = await _empresaApp.ListBySociosAsync(Input.Socios);

                    Input = new InputModel
                    {
                        ListaEmpresas = emp
                    };
                }

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }

        public async Task<JsonResult> OnGetDetalheEmpresa(string id)
        {
            var t = await _empresaApp.GetCnpjAsync(id);

            return new JsonResult(t);
        }
    }
}
