using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Sim.UI.Web.Pages.Triagem
{
    
    using Sim.Cross.Identity;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex Input { get; set; }

        public class InputModelIndex
        {
            public IEnumerable<ApplicationUser> ListaUsuariosPAT { get; set; }
            public IEnumerable<ApplicationUser> ListaUsuariosBPP { get; set; }
            public IEnumerable<ApplicationUser> ListaUsuariosSA { get; set; }
            public IEnumerable<ApplicationUser> ListaUsuariosSE { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private async Task LoadAsync()
        {
            Input = new()
            {                
                ListaUsuariosPAT = await _userManager.GetUsersInRoleAsync("M_Pat"),
                ListaUsuariosBPP = await _userManager.GetUsersInRoleAsync("M_BancoPovo"),
                ListaUsuariosSA = await _userManager.GetUsersInRoleAsync("M_Sebrae"),
                ListaUsuariosSE = await _userManager.GetUsersInRoleAsync("M_SalaEmpreendedor")
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }
    }
}
