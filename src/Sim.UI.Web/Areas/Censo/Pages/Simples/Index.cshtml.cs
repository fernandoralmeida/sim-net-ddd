using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interface;

namespace Sim.UI.Web.Areas.Censo.Pages.Simples
{
    public class IndexModel : PageModel
    {
        private readonly ICNPJBase<BaseReceitaFederal> _empresaApp;
        private readonly IServiceCnpj<BaseReceitaFederal> _appServiceCNPJ;
        private readonly IServiceSimplesNacional<BaseReceitaFederal> _appsimples;
        private readonly IBase<Municipio> _municipios;

        public IndexModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa,
            IServiceCnpj<BaseReceitaFederal> appServiceCNPJ,
            IBase<Municipio> municipios,
            IServiceSimplesNacional<BaseReceitaFederal> appsimples)
        {
            _empresaApp = appServiceEmpresa;
            _appServiceCNPJ = appServiceCNPJ;
            _appsimples = appsimples;
            _municipios = municipios;
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

            [DisplayName("Situação")]
            public string Situacao { get; set; }
            public string Municipio { get; set; }
            public IEnumerable<BaseReceitaFederal> ListaEmpresas { get; set; }
            public COptanteSimples VGOptantesSimples { get; set; }
        }

        public class COptanteSimples
        {
            public string OptSimples { get; set; }
            public CSituacao ListaOptSimples { get; set; }
            public string OptMEI { get; set; }
            public CSituacao ListaOptSMEI { get; set; }
            public string OptSimplesNaoMEI { get; set; }
            public CSituacao ListaOptSimplesNaoMEI { get; set; }
            public string ExclusaoSimples { get; set; }
            public CSituacao ListaExclusaoSimples { get; set; }
        }
        public class CSituacao
        {
            public string Ativa { get; set; }
            public string Inapta { get; set; }
            public string Suspensa { get; set; }
            public string Nula { get; set; }
            public string Baixada { get; set; }
        }
        
        private async Task LoadMunicipios()
        {

            var t = await _municipios.ListAll();

            if (t != null)
            {
                ListaMunicipios = new SelectList(t, nameof(Municipio.Codigo), nameof(Municipio.Descricao), null);
            }
        }

        private async Task LoadAsync()
        {
            var emp = await _empresaApp.ListAllOptanteSimplesAsync(Input.Municipio);

            var t = await _appsimples.OptanteSimplesNacional(emp);

            var v = await _appServiceCNPJ.EmpresasAtivas(t);

            Input.ListaEmpresas = v;

            StatusMessage = "Jaú tem no total geral " + emp.Count().ToString() + " CNPJs Optantes do Simples Nacional";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new();
            Input.Municipio = "6607";
            await LoadMunicipios();
            //await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var emp = await _empresaApp.ListAllOptanteSimplesAsync(Input.Municipio);

                var t1 = _appsimples.OptanteSimplesNacional(emp);
                var a1 = _appServiceCNPJ.EmpresasAtivas(t1.Result);
                var n1  = _appServiceCNPJ.EmpresasNulas(t1.Result);
                var s1 = _appServiceCNPJ.EmpresasSuspensas(t1.Result);
                var i1 = _appServiceCNPJ.EmpresasInaptas(t1.Result);
                var b1 = _appServiceCNPJ.EmpresasBaixadas(t1.Result);

                var t2 = _appsimples.OptanteSimplesNacionalNaoMEI(emp);
                var a2 = _appServiceCNPJ.EmpresasAtivas(t2.Result);
                var n2 = _appServiceCNPJ.EmpresasNulas(t2.Result);
                var s2 = _appServiceCNPJ.EmpresasSuspensas(t2.Result);
                var i2 = _appServiceCNPJ.EmpresasInaptas(t2.Result);
                var b2 = _appServiceCNPJ.EmpresasBaixadas(t2.Result);

                var t3 = _appsimples.OptanteMEI(emp);
                var a3 = _appServiceCNPJ.EmpresasAtivas(t3.Result);
                var n3 = _appServiceCNPJ.EmpresasNulas(t3.Result);
                var s3 = _appServiceCNPJ.EmpresasSuspensas(t3.Result);
                var i3 = _appServiceCNPJ.EmpresasInaptas(t3.Result);
                var b3 = _appServiceCNPJ.EmpresasBaixadas(t3.Result);

                var t4 = _appsimples.ExclusaoSimplesNacional(emp);
                var a4 = _appServiceCNPJ.EmpresasAtivas(t4.Result);
                var n4 = _appServiceCNPJ.EmpresasNulas(t4.Result);
                var s4 = _appServiceCNPJ.EmpresasSuspensas(t4.Result);
                var i4 = _appServiceCNPJ.EmpresasInaptas(t4.Result);
                var b4 = _appServiceCNPJ.EmpresasBaixadas(t4.Result);

                var op1 = new COptanteSimples()
                {
                    OptSimples = t1.Result.Count().ToString(),
                    ListaOptSimples = new CSituacao() { 
                        Ativa =  a1.Result.Count().ToString(),
                        Nula =n1.Result.Count().ToString(),
                        Suspensa = s1.Result.Count().ToString(),
                        Inapta = i1.Result.Count().ToString(),
                        Baixada = b1.Result.Count().ToString()                    
                    },
                    OptSimplesNaoMEI = t2.Result.Count().ToString(),
                    ListaOptSimplesNaoMEI=new  CSituacao() {
                        Ativa = a2.Result.Count().ToString(),
                        Nula = n2.Result.Count().ToString(),
                        Suspensa = s2.Result.Count().ToString(),
                        Inapta = i2.Result.Count().ToString(),
                        Baixada = b2.Result.Count().ToString()
                    },
                    OptMEI = t3.Result.Count().ToString(),
                    ListaOptSMEI = new CSituacao() {
                        Ativa = a3.Result.Count().ToString(),
                        Nula = n3.Result.Count().ToString(),
                        Suspensa = s3.Result.Count().ToString(),
                        Inapta = i3.Result.Count().ToString(),
                        Baixada = b3.Result.Count().ToString()
                    },
                    ExclusaoSimples = t4.Result.Count().ToString(),
                    ListaExclusaoSimples = new CSituacao() {
                        Ativa = a4.Result.Count().ToString(),
                        Nula = n4.Result.Count().ToString(),
                        Suspensa = s4.Result.Count().ToString(),
                        Inapta = i4.Result.Count().ToString(),
                        Baixada = b4.Result.Count().ToString()
                    }
                };
                
                await LoadMunicipios();
                Input.VGOptantesSimples = op1;
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }
    }
}
