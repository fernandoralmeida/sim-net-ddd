using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Domain.Shared.Entity;
    using Sim.Application.Shared.Interface;
    public class IndexModel : PageModel
    {


        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [DataType(DataType.Date)]
            public DateTime? DataAtendimento { get; set; }

            public ICollection<Atendimento> ListaAtendimento { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new();

            return Page();
        }
    }
}
