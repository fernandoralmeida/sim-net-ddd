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

    public class ServicosModel : PageModel
    {
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        private readonly IAppServiceServico _appServiceServico;
        public ServicosModel(IAppServiceSetor appServiceSetor,
            IAppServiceSecretaria appServiceSecretaria,
            IAppServiceServico appServiceServico)
        {
            _appServiceSetor = appServiceSetor;
            _appServiceSecretaria = appServiceSecretaria;
            _appServiceServico = appServiceServico;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMServico Input { get; set; }

        [BindProperty]
        public Guid ItemSelecionado { get; set; }

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
            var serv = Task.Run(() => _appServiceServico.List());
            await serv;

            Input = new VMServico()
            {
                Listar = serv.Result.ToList(),
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                { return Page(); }

                var t = Task.Run(() =>
                {

                    var sec = _appServiceSecretaria.GetById(ItemSelecionado);
                    var set = _appServiceSetor.GetById(SetorSelecionado);

                    var input = new Servico()
                    {
                        Nome = Input.Nome,
                        Secretaria = sec,
                        Setor = set,
                        Ativo = true
                    };

                    _appServiceServico.Add(input);

                });

                await t;

                StatusMessage = "Serviço incluído com sucesso!";

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluí novo serviço!" + "\n" + ex.Message;

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

                    var serv =  _appServiceServico.GetById(id);
                                      
                    _appServiceServico.Remove(serv);

                });

                await t;

                //StatusMessage = "Serviço incluído com sucesso!";

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluí novo serviço!" + "\n" + ex.Message;

                return RedirectToPage();
            }

        }
    }
}
