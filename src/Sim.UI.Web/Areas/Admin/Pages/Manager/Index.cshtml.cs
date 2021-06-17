using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{
    using ViewModel;
    using Sim.Cross.Identity;

    [Authorize(Roles = "Administrador")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceUser _appIdentity;
        public IndexModel(IAppServiceUser appServiceUser)
        {
            _appIdentity = appServiceUser;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMListUsers Input { get; set; }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => _appIdentity.GetAll());
            await t;

            Input = new()
            {
                Users = t.Result
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }
    }
}
