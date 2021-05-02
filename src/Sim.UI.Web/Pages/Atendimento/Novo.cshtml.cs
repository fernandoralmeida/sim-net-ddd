using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.Shared.Interface;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.SDE.Entity;
    using Sim.Cross.Identity;

    public class NovoModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;

        public NovoModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSetor appServiceSetor,
            UserManager<ApplicationUser> userManager)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceSetor = appServiceSetor;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList Setores { get; set; }

        public SelectList Servicos { get; set; }

        public SelectList Canais { get; set; }

        public SelectList ServicosSelecionado { get; set; }

        private async Task OnLoad()
        {
            var set = Task.Run(() => _appServiceSetor.List());
            await set;
            var serv = Task.Run(() => _appServiceServico.List());
            await serv;
            var can = Task.Run(() => _appServiceCanal.List());
            await can;

            if (set.Result != null)
            {
                Setores = new SelectList(set.Result, nameof(Setor.Id), nameof(Setor.Nome), null);
            }

            if (can.Result != null)
            {
                Canais = new SelectList(can.Result, nameof(Canal.Id), nameof(Canal.Nome), null);
            }

            if (serv.Result != null)
            {
                Servicos = new SelectList(serv.Result, nameof(Servico.Id), nameof(Servico.Nome), null);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(user.Id).FirstOrDefault();

            if(atendimemnto_ativio == null)
            {
                return Redirect("Erro inesperado, contate o Administrador.");
            }

            Input = new()
            {
                Id= atendimemnto_ativio.Id,
                Protocolo = atendimemnto_ativio.Protocolo,               
                Data = atendimemnto_ativio.Data,
                Pessoa = atendimemnto_ativio.Pessoa,
                Empresa = atendimemnto_ativio.Empresa,
                Status = atendimemnto_ativio.Status
            };       

            await OnLoad();
            return Page();
        }

        public async Task<IActionResult> OnPostSyncServicesAsync()
        {
            var serv = Task.Run(() => _appServiceServico.GetByOwner(Input.Setor));
            await serv;

            if (serv.Result != null)
            {
                Servicos = new SelectList(serv.Result, nameof(Servico.Id), nameof(Servico.Nome), null);
            }
            
            return RedirectToPage();
        }

        public IActionResult OnPostAddServiceAsync()
        {

            //StatusMessage = Input.Servicos;

            return Page();
        }

        public void OnPostRemoveServiceAsync()
        {

        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            var t = Task.Run(() => { });
            await t;
            StatusMessage = "TESTE";

            return RedirectToPagePreserveMethod();
        }

    }
}
