using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    using ViewModel.Common;
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EventosModel : PageModel
    {

        private readonly IAppServiceEvento _appServiceEvento;
        public EventosModel(IAppServiceEvento appServiceEvento)
        {

            _appServiceEvento = appServiceEvento;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMEvento Input { get; set; }

        private async Task OnLoad()
        {
            var eve = Task.Run(() => _appServiceEvento.List());
            await eve;

            Input = new VMEvento()
            {
                Listar = eve.Result.ToList(),
                Ativo = true
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                { return Page(); }

                var t = Task.Run(() =>
                {

                    var input = new Evento()
                    {
                        Nome = Input.Nome,
                        Tipo = Input.Tipo,
                        Ativo = true
                    };

                    _appServiceEvento.Add(input);

                });

                await t;

                StatusMessage = "Tipo incluído com sucesso!";

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluí novo tipo!" + "\n" + ex.Message;

                return RedirectToPage();
            }

        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                { return Page(); }

                var t = Task.Run(() =>
                {

                    var tipo = _appServiceEvento.GetById(id);

                    _appServiceEvento.Remove(tipo);

                });

                await t;

                //StatusMessage = "Tipo removido com sucesso!";

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluí novo tipo!" + "\n" + ex.Message;

                return RedirectToPage();
            }

        }
    }
}
