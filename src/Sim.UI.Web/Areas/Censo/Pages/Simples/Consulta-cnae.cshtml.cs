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

namespace Sim.UI.Web.Areas.Censo.Pages.Simples
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;
    using Sim.Application.Interface;


    [Authorize(Roles = "Administrador,M_RFB")]
    public class Consulta_cnaeModel : PageModel
    {
        private readonly ICNPJBase<BaseReceitaFederal> _empresaApp;
        private readonly IBase<Municipio> _municipios;
        private readonly IBase<CNAE> _cnaes;
        private readonly IServiceCnpj<BaseReceitaFederal> _appServiceCNPJ;
        private readonly IServiceSimplesNacional<BaseReceitaFederal> _appsimples;
        public Consulta_cnaeModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa,
            IBase<Municipio> municipios,
            IBase<CNAE> cnaes,
            IServiceCnpj<BaseReceitaFederal> appServiceCNPJ,
            IServiceSimplesNacional<BaseReceitaFederal> appsimples)
        {
            _empresaApp = appServiceEmpresa;
            _municipios = municipios;
            _cnaes = cnaes;
            _appServiceCNPJ = appServiceCNPJ;
            _appsimples = appsimples;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList ListaMunicipios { get; set; }

        public SelectList ListaCnaes { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [DisplayName("Optante Simples Nacional")]
            public string OptanteSimples { get; set; }

            [DisplayName("CNAE")]
            public string CNAE { get; set; }

            public string Situacao { get; set; }

            public IEnumerable<BaseReceitaFederal> ListaEmpresas { get; set; }

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
                    await LoadCnaes();

                    var emp = await _empresaApp.ListByAtividadeAsync(Input.CNAE, Input.Municipio);

                    IEnumerable<BaseReceitaFederal> t = new List<BaseReceitaFederal>();

                    switch (Input.OptanteSimples)
                    {
                        case "opsimples":
                            t = await _appsimples.OptanteSimplesNacional(emp);
                            break;

                        case "opsimplesnaomei":
                            t = await _appsimples.OptanteSimplesNacionalNaoMEI(emp);
                            break;

                        case "opmei":
                            t = await _appsimples.OptanteMEI(emp);
                            break;

                        case "excsimples":
                            t = await _appsimples.ExclusaoSimplesNacional(emp);
                            break;


                        case "excsimplesmei":
                            t = await _appsimples.ExclusaoSimplesNacionalMEI(emp);
                            break;

                        default:
                            break;
                    }

                    IEnumerable<BaseReceitaFederal> v = new List<BaseReceitaFederal>();

                    switch (Input.Situacao)
                    {
                        case "Ativa":
                            v = await _appServiceCNPJ.EmpresasAtivas(t);
                            break;

                        case "Nula":
                            v = await _appServiceCNPJ.EmpresasNulas(t);
                            break;

                        case "Suspensa":
                            v = await _appServiceCNPJ.EmpresasSuspensas(t);
                            break;

                        case "Inapta":
                            v = await _appServiceCNPJ.EmpresasInaptas(t);
                            break;

                        case "Baixada":
                            v = await _appServiceCNPJ.EmpresasBaixadas(t);
                            break;

                        default:
                            break;
                    }

                    Input = new InputModel
                    {
                        ListaEmpresas = v
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
