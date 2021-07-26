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

    [Authorize(Roles = "Administrador,M_RFB")]
    public class Consulta_bairroModel : PageModel
    {
        private readonly ICNPJBase<BaseReceitaFederal> _empresaApp;
        private readonly IServiceMunicipios<Municipio> _municipios;
        private readonly IServiceCnpj<BaseReceitaFederal> _appServiceCNPJ;
        private readonly IServiceSimplesNacional<BaseReceitaFederal> _appsimples;
        public Consulta_bairroModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa,
            IServiceMunicipios<Municipio> municipios,
            IServiceCnpj<BaseReceitaFederal> appServiceCNPJ,
            IServiceSimplesNacional<BaseReceitaFederal> appsimples)
        {
            _empresaApp = appServiceEmpresa;
            _municipios = municipios;
            _appServiceCNPJ = appServiceCNPJ;
            _appsimples = appsimples;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList ListaMunicipios { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [DisplayName("Optante Simples Nacional")]
            public string OptanteSimples { get; set; }

            [Required]
            [DisplayName("Bairro")]
            public string Bairro { get; set; }

            public string Situacao { get; set; }

            public IEnumerable<BaseReceitaFederal> ListaEmpresas { get; set; }

            [Required]
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

        public async Task OnGetAsync()
        {
            await LoadMunicipios();
            Input = new() { Municipio = "6607", ListaEmpresas = new List<BaseReceitaFederal>().ToList() };
        }

        public async Task OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = await _empresaApp.ListByBairroAsync(Input.Bairro, Input.Municipio);

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

                    Input.ListaEmpresas = v;

                }

                await LoadMunicipios();

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            //return Page();
        }
    }
}
