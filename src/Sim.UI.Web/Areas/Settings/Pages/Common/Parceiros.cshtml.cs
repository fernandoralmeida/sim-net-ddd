using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{

    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;

    public class ParceirosModel : PageModel
    {

        private readonly IAppServiceSecretaria _appServiceSecretaria;
        private readonly IAppServiceParceiro _appServiceParceiro;
        public ParceirosModel(IAppServiceSecretaria appServiceSecretaria,
            IAppServiceParceiro appServiceParceiro)
        {
            _appServiceSecretaria = appServiceSecretaria;
            _appServiceParceiro = appServiceParceiro;
        }


        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Parceiro")]
            public string Nome { get; set; }

            [DisplayName("Secretaria")]
            public Secretaria Secretaria { get; set; } //Secretaria

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }
            public virtual ICollection<Parceiro> Listar { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public Guid SecretariaSelecionada { get; set; }

        [BindProperty]
        public Guid SetorSelecionado { get; set; }

        public SelectList Secretarias { get; set; }

        public SelectList Setores { get; set; }

        private async Task OnLoad()
        {
            var sec = Task.Run(() => _appServiceSecretaria.List());
            await sec;

            var parc = Task.Run(() => _appServiceParceiro.List());
            await parc;

            Input = new InputModel()
            {
                Listar = parc.Result.ToList(),
                Ativo = true
            };

            if (sec.Result != null)
            {
                Secretarias = new SelectList(sec.Result, nameof(Secretaria.Id), nameof(Secretaria.Nome), null);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();
            return Page();
        }

        public async Task OnPostAddAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var t = Task.Run(() =>
                    {

                        var sec = _appServiceSecretaria.GetById(SecretariaSelecionada);

                        var input = new Parceiro()
                        {
                            Nome = Input.Nome,
                            Secretaria = sec,
                            Ativo = true
                        };

                        _appServiceParceiro.Add(input);

                    });

                    await t;

                    Input.Nome = string.Empty;
                }
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo parceiro!" + "\n" + ex.Message;

            }

        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {

                var t = Task.Run(() =>
                {

                    var canal = _appServiceParceiro.GetById(id);

                    _appServiceParceiro.Remove(canal);

                });

                await t;
                await OnLoad();

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover parceiro!" + "\n" + ex.Message;

            }
        }
    }
}
