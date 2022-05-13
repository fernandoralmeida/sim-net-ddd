using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Pat
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpregos appEmpregos;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string CNPJ { get; set; }
        public ICollection<Empregos> ListaEmpregos { get; set; }

        public IndexModel(IAppServiceEmpregos appServiceEmpregos)
        {
            appEmpregos = appServiceEmpregos;
        }
        public void OnGet()
        {
            ListaEmpregos = (ICollection<Empregos>)appEmpregos.GetAllEmpregosAsync().Result;
        }
    }
}
