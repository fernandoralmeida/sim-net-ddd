using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;

    public class EventosModel : PageModel
    {

        private readonly IAppServiceTipo _appServiceTipo;
        public EventosModel(IAppServiceTipo appServiceTipo)
        {

            _appServiceTipo = appServiceTipo;
        }

        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [DisplayName("Nome")]
            public string Nome { get; set; }

            [DisplayName("Tipo")]
            public string Tipo { get; set; } //Tipo

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }

            public virtual ICollection<Tipo> Listar { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        private async Task OnLoad()
        {
            var eve = Task.Run(() => _appServiceTipo.List());
            await eve;

            Input = new InputModel()
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

                    var input = new Tipo()
                    {
                        Nome = Input.Nome,
                        Owner = Input.Tipo,
                        Ativo = true
                    };

                    _appServiceTipo.Add(input);

                });

                await t;

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo tipo!" + "\n" + ex.Message;

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

                    var tipo = _appServiceTipo.GetById(id);

                    _appServiceTipo.Remove(tipo);

                });

                await t;

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover tipo!" + "\n" + ex.Message;

                return RedirectToPage();
            }

        }
    }
}
