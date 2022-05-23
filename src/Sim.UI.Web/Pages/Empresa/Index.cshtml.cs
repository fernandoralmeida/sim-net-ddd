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

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public ParamModel GetParam { get; set; }

        public class InputModel
        {
            
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }
            public string CNAE { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public IEnumerable<Empresas> ListaEmpresas { get; set; }
        }

        public class ParamModel
        {
            public string Param1 { get; set; }
            public string Param2 { get; set; }
            public string Param3 { get; set; }
            public string Param4 { get; set; }
            public string Param5 { get; set; }
        }

        public void OnGetAsync()
        {        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    var param = new List<object>() {
                    Input.CNPJ,
                    Input.RazaoSocial,
                    Input.CNAE,
                    Input.Logradouro,
                    Input.Bairro};

                    Input.ListaEmpresas = await _empresaApp.ListByParam(param);                    

                    GetParam.Param1 = (string)param[0];
                    GetParam.Param2 = (string)param[1] != null ? (string)param[1] : "0";
                    GetParam.Param3 = (string)param[2] != null ? (string)param[2] : "0";
                    GetParam.Param4 = (string)param[3] != null ? (string)param[3] : "0";
                    GetParam.Param5 = (string)param[4] != null ? (string)param[4] : "0";
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
