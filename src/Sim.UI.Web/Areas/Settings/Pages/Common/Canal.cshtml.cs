using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class CanalModel : PageModel
    {
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        private readonly IAppServiceCanal _appServiceCanal;
        public CanalModel(IAppServiceSetor appServiceSetor,
            IAppServiceSecretaria appServiceSecretaria,
            IAppServiceCanal appServiceCanal)
        {
            _appServiceSetor = appServiceSetor;
            _appServiceSecretaria = appServiceSecretaria;
            _appServiceCanal = appServiceCanal;
        }

        
        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Canal")]
            public string Nome { get; set; }

            [DisplayName("Secretaria")]
            public Secretaria Secretaria { get; set; } //Secretaria

            [DisplayName("Setor")]
            public Setor Setor { get; set; } //Setor

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }
            public virtual ICollection<Canal> Listar { get; set; }
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
            var set = Task.Run(() => _appServiceSetor.List());
            await set;
            var ca = Task.Run(() => _appServiceCanal.List());
            await ca;

            Input = new InputModel()
            {
                Listar = ca.Result.ToList(),
                Ativo = true
            };

            if (sec.Result != null)
            {
                Secretarias = new SelectList(sec.Result, nameof(Secretaria.Id), nameof(Secretaria.Nome), null);
            }

            if (set.Result != null)
            {
                Setores = new SelectList(set.Result, nameof(Setor.Id), nameof(Setor.Nome), null);
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
                        var set = _appServiceSetor.GetById(SetorSelecionado);

                        var input = new Canal()
                        {
                            Nome = Input.Nome,
                            Secretaria = sec,
                            Setor = set,
                            Ativo = true
                        };

                        _appServiceCanal.Add(input);

                    });

                    await t;
                    
                    Input.Nome = string.Empty;
                }
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo canal!" + "\n" + ex.Message;

            }

        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {

                var t = Task.Run(() =>
                {

                    var canal = _appServiceCanal.GetById(id);                  

                    _appServiceCanal.Remove(canal);

                });

                await t;
                await OnLoad();

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover canal!" + "\n" + ex.Message;

            }
        }
    }


}
