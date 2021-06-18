using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{

    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;

    public class IndexModel : PageModel
    {
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        public IndexModel(IAppServiceSecretaria appServiceSecretaria)
        {
            _appServiceSecretaria = appServiceSecretaria;
        }

        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Nome")]
            public string Nome { get; set; }

            [DisplayName("Unidade Responsável")]
            public string Owner { get; set; } //Prefeitura

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }

            public virtual ICollection<Secretaria> Listar { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        
        private async Task OnLoad()
        {
            var t = Task.Run(() => _appServiceSecretaria.List());
            await t;
            Input = new InputModel()
            {
                Listar = t.Result.ToList(),
                Ativo = true
            };
        }

        public void OnGet()
        {
            OnLoad().Wait();
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {

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

            }

            await OnLoad();
        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {


                var t = Task.Run(() =>
                {
                    var sec = _appServiceSecretaria.GetById(id);
                    _appServiceSecretaria.Remove(sec);
                });

                await t;
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover Secretaria!" + "\n" + ex.Message;
            }
        }
    }
}
