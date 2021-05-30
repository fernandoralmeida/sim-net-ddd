using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Domain.Shared.Entity;
    using Sim.Application.Shared.Interface;
    using Sim.Cross.Identity;


    [Authorize]
    public class ConsultaPendenciasModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        public ConsultaPendenciasModel(IAppServiceAtendimento appServiceAtendimento,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _appServiceAtendimento = appServiceAtendimento;
            Input = new();
            Input.DataI = new DateTime(DateTime.Now.Year, 1, 1);
            Input.DataF = DateTime.Now;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = await _userManager.GetUserAsync(User);
            var lista = Task.Run(() => _appServiceAtendimento.AtendimentoAtivo(userId.Id));
            await lista;
            Input.ListaAtendimento = lista.Result.ToList();

            return Page();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string CPF { get; set; }

            public string CNPJ { get; set; }

            public ICollection<Atendimento> ListaAtendimento { get; set; }
        }

        public async Task OnPostListPendenciasAsync()
        {
            var userId = _userManager.Users.FirstOrDefault().Id;

            var lista = Task.Run(() => _appServiceAtendimento.AtendimentoAtivo(userId));
            await lista;
            Input.ListaAtendimento = lista.Result.ToList();
        }
    }
}
