using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{
    using ViewModel;
    public class ClaimsModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMRoleClaims Input { get; set; }

        public void OnGet()
        {
        }
    }
}
