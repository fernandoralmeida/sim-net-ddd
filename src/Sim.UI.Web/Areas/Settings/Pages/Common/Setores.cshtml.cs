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

    public class SetoresModel : PageModel
    {
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        public SetoresModel(IAppServiceSetor appServiceSetor, IAppServiceSecretaria appServiceSecretaria)
        {
            _appServiceSetor = appServiceSetor;
            _appServiceSecretaria = appServiceSecretaria;
        }

        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [DisplayName("Nome")]
            public string Nome { get; set; }

            [DisplayName("Secretaria")]
            public Secretaria Secretaria { get; set; } //Secretaria

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }

            public virtual ICollection<Setor> Listar { get; set; }

        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public Guid ItemSelecionado { get; set; }

        public SelectList Secretarias { get; set; }

        private async Task OnLoad()
        {
            var s = Task.Run(() => _appServiceSecretaria.List());
            await s;
            var t = Task.Run(() => _appServiceSetor.List());
            await t;

            Input = new InputModel()
            {
                Listar = t.Result.ToList(),
                Ativo = true
            };
            
            if(s.Result != null)
            {
                Secretarias = new SelectList(s.Result, nameof(Secretaria.Id), nameof(Secretaria.Nome), null);
            }            
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

                    var sec = _appServiceSecretaria.GetById(ItemSelecionado);

                    var input = new Setor()
                    {
                        Nome = Input.Nome,
                        Secretaria = sec,
                        Ativo = true
                    };

                    _appServiceSetor.Add(input);

                });

                await t;

                return RedirectToPage();
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo setor!" + "\n" + ex.Message;

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

                    var set = _appServiceSetor.GetById(id);
                    
                    _appServiceSetor.Remove(set);

                });

                await t;

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover setor!" + "\n" + ex.Message;

                return RedirectToPage();
            }

        }
    }
}
