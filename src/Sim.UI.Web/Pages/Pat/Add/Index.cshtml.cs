using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sim.UI.Web.Pages.Pat.Add
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Functions;
    using System;

    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceEmpregos _appServiceEmpregos;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public IndexModel(IAppServiceEmpresa appServiceEmpresa,
            IAppServiceEmpregos appServiceEmpregos) {
        
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceEmpregos = appServiceEmpregos;

        }

        private async void GetCNPJ(Guid cnpj)
        {

                var t = Task.Run(() => _appServiceEmpresa.GetById(cnpj));

                await t;

                
                    Input.Empresa.Nome_Empresarial = t.Result.Nome_Empresarial;
                    Input.Empresa.CNAE_Principal = t.Result.CNAE_Principal;
                    Input.Empresa.Atividade_Principal = t.Result.Atividade_Principal;
                

        }

        public void OnGet(Guid id)
        {
            Input.Data = DateTime.Now;
            GetCNPJ(id);            
        }

        public IActionResult OnPostCNPJ()
        {
            if (Input.Empresa != null)
                return RedirectToPage("/Pat/Add/Index", new { id = Input.Empresa.CNPJ.MaskRemove() });
            else
                return Page();

        }

    }
}
