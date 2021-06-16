using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Areas.Censo.Pages.Empresas
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;

    using Sim.Application.Interface;


    [Authorize(Roles = "Administrador,M_RFB")]
    public class IndexModel : PageModel
    {
        private readonly ICNPJBase<BaseReceitaFederal>  _empresaApp;

        public IndexModel(ICNPJBase<BaseReceitaFederal> appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Required]
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }

            public IEnumerable<BaseReceitaFederal> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            public string CNPJRes { get; set; }
        }

        private async Task LoadAsync()
        {
            var t = await _empresaApp.ListTop10();

            Input = new InputModel
            {
                ListaEmpresas = t
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = await  _empresaApp.ListByCnpjAsync(Input.CNPJ);                                       

                    Input = new InputModel
                    {
                        ListaEmpresas = emp,
                        CNPJRes = new Functions.Mask().Remove(Input.CNPJ)                       
                        
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
