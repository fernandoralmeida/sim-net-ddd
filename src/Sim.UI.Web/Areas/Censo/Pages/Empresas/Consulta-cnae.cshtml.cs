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
    using Sim.Application.Interface;


    [Authorize(Roles = "Administradir")]
    [Authorize(Roles = "M_RFB")]
    public class Consulta_cnaeModel : PageModel
    {
        private readonly ICNPJBase<BaseReceitaFederal> _empresaApp;
        private readonly IBase<Municipio> _municipios;
        private readonly IBase<CNAE> _cnaes;
        public Consulta_cnaeModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa,
            IBase<Municipio> municipios,
            IBase<CNAE> cnaes)
        {
            _empresaApp = appServiceEmpresa;
            _municipios = municipios;
            _cnaes = cnaes;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList ListaMunicipios { get; set; }

        public SelectList ListaCnaes { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("CNAE")]
            public string CNAE { get; set; }

            public IEnumerable<BaseReceitaFederal> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            public string Municipio { get; set; }
        }

        private async Task LoadCnaes()
        {
            var t = await _cnaes.ListAll();

            if (t != null)
            {
                ListaCnaes = new SelectList(t, nameof(CNAE.Codigo), nameof(CNAE.Descricao), null);
            }
        }

        private async Task LoadMunicipios()
        {

            var t = await _municipios.ListAll();

            if (t != null)
            {
                ListaMunicipios = new SelectList(t, nameof(Municipio.Codigo), nameof(Municipio.Descricao), null);
            }
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await LoadCnaes();
            await LoadMunicipios();

            var emp = await _empresaApp.ListByCnpjAsync("99999999999999");

            Input = new InputModel
            {
                Municipio = "6607",
                ListaEmpresas = emp
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
                    await LoadCnaes();

                    var emp = await _empresaApp.ListByAtividadeAsync(Input.CNAE, Input.Municipio);

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
    }
}
