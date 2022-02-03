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

    [Authorize(Roles = "Administrador,M_RFB")]
    public class Consulta_logradouroModel : PageModel
    {
        private readonly ICNPJBase<BaseReceitaFederal> _empresaApp;
        private readonly IServiceMunicipios<Municipio> _municipios;
        private readonly IServiceCnpj<BaseReceitaFederal> _appServiceCNPJ;

        public Consulta_logradouroModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa,
            IServiceMunicipios<Municipio> municipios,
            IServiceCnpj<BaseReceitaFederal> appServiceCNPJ)
        {
            _empresaApp = appServiceEmpresa;
            _municipios = municipios;
            _appServiceCNPJ = appServiceCNPJ;
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

            [Required]
            [DisplayName("Logradouro")]
            public string Logradouro { get; set; }

            public string Situacao { get; set; }
            public IEnumerable<BaseReceitaFederal> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await LoadMunicipios();

                    var emp = await _empresaApp.ListByLogradouroAsync(Input.Logradouro, Input.Municipio);

                    IEnumerable<BaseReceitaFederal> t = new List<BaseReceitaFederal>();

                    switch (Input.Situacao)
                    {
                        case "Ativa":
                            t = await _appServiceCNPJ.EmpresasAtivas(emp);
                            break;

                        case "Nula":
                            t = await _appServiceCNPJ.EmpresasNulas(emp);
                            break;

                        case "Suspensa":
                            t = await _appServiceCNPJ.EmpresasSuspensas(emp);
                            break;

                        case "Inapta":
                            t = await _appServiceCNPJ.EmpresasInaptas(emp);
                            break;

                        case "Baixada":
                            t = await _appServiceCNPJ.EmpresasBaixadas(emp);
                            break;

                        case "Geral":
                            t = emp;
                            break;

                        default:
                            break;
                    }

                    Input = new InputModel
                    {
                        ListaEmpresas = t
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
