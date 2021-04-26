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

        private async Task LoadAsync(DateTime? date)
        {
            Input = new();
            Input.DataAtendimento = date;
            var t = Task.Run(() => _appServiceAtendimento.GetByDate(Input.DataAtendimento));
            await t;
            Input.ListaAtendimento = t.Result.ToList();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync(DateTime.Now.Date);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadAsync(Input.DataAtendimento);

            if(Input.ListaAtendimento.Count == 0)
            {
                StatusMessage = string.Format("Erro: Não há atendimentos para do {0}", Input.DataAtendimento);
            }

            return Page();
        }
    }
}
