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
    public class IndexModel : PageModel
    {
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        public IndexModel(IAppServiceSecretaria appServiceSecretaria)
        {
            _appServiceSecretaria = appServiceSecretaria;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMSecretaria Input { get; set; }
        
        private async Task OnLoad()
        {
            var t = Task.Run(() => _appServiceSecretaria.List());
            await t;
            Input = new VMSecretaria()
            {
                Listar = t.Result.ToList(),
                Ativo = true
            };
        }

        public void OnGet()
        {
            OnLoad().Wait();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            { return Page(); }

            var t = Task.Run(() =>
            {

                var input = new Secretaria()
                {
                    Nome = Input.Nome,
                    Owner = Input.Owner,
                    Ativo = true
                };

                _appServiceSecretaria.Add(input);

            });

            await t;

            StatusMessage = "Secretaria incluída com sucesso!";
                        
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                { return Page(); }

                var t = Task.Run(() =>
                {
                    var sec = _appServiceSecretaria.GetById(id);

                    _appServiceSecretaria.Remove(sec);

                });

                await t;

                StatusMessage = "Secretaria removida com sucesso!";

                return RedirectToPage();
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro ao tentar remover Secretaria!" + "\n" + ex.Message;

                return Page();
            }
        }
    }
}
