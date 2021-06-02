using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.SebraeAqui
{
    using Sim.Cross.Identity;
    using Sim.Domain.Shared.Entity;
    using Sim.Application.Shared.Interface;

    [Authorize]
    public class RaeLancadosModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public RaeLancadosModel(IAppServiceAtendimento appServiceAtendimento,
            UserManager<ApplicationUser> userManager)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _userManager = userManager;
            Input = new();
            Input.DataAtendimento = DateTime.Now.Date;
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

        private async Task LoadAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var t = Task.Run(() => _appServiceAtendimento.ListarRaeLancados(user.Id));
            await t;
            Input.ListaAtendimento = t.Result.ToList();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            await LoadAsync();
            if (Input.ListaAtendimento.Count == 0)
            {
                StatusMessage = string.Format("Erro: N�o h� atendimentos para do {0}", Input.DataAtendimento.Value.Date);
            }

            return Page();
        }
    }
}
