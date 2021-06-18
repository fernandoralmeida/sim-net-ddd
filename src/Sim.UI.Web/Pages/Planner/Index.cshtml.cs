using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Planner
{
    [Authorize(Roles = "Administrador")]
    public class IndexModel : PageModel
    {
        public IndexModel() { }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelPlanner Input { get; set; }

        public void OnGet()
        {

        }
    }
}
