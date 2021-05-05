using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.Shared.Interface;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.SDE.Entity;
    using Sim.Cross.Identity;

    [Authorize]
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
                Setores = new SelectList(set.Result, nameof(Setor.Nome), nameof(Setor.Nome), null);
            }

            if (can.Result != null)
            {
                Canais = new SelectList(can.Result, nameof(Canal.Nome), nameof(Canal.Nome), null);
            }

            if (serv.Result != null)
            {
                Servicos = new SelectList(serv.Result, nameof(Servico.Nome), nameof(Servico.Nome), null);
                //ViewData["ListaID"] = new SelectList(serv.Result, nameof(Servico.Nome), nameof(Servico.Nome), null);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();

            var user = await _userManager.GetUserAsync(User);

            var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(user.Id).FirstOrDefault();

            if(atendimemnto_ativio == null)
            {
                return RedirectToPage("./Index");
            }

            Input = new()
            {
                Id= atendimemnto_ativio.Id,
                Protocolo = atendimemnto_ativio.Protocolo,               
                Data = atendimemnto_ativio.Data,
                DataF = atendimemnto_ativio.DataF,
                Setor = atendimemnto_ativio.Setor,
                Canal = atendimemnto_ativio.Canal,
                Servicos = atendimemnto_ativio.Servicos,
                Descricao = atendimemnto_ativio.Descricao,
                Status = atendimemnto_ativio.Status,
                Ultima_Alteracao =atendimemnto_ativio.Ultima_Alteracao,
                Ativo =atendimemnto_ativio.Ativo,
                Owner_AppUser_Id = atendimemnto_ativio.Owner_AppUser_Id,
                Pessoa = atendimemnto_ativio.Pessoa,
                Empresa = atendimemnto_ativio.Empresa
            };       

            
            return Page();
        }

        public async Task<IActionResult> OnPostSyncServicesAsync()
        {
            var serv = Task.Run(() => _appServiceServico.GetByOwner(Input.Setor));
            await serv;

            if (serv.Result != null)
            {
                Servicos = new SelectList(serv.Result, nameof(Servico.Nome), nameof(Servico.Nome), null);
            }
            
            return RedirectToPage();
        }

        public IActionResult OnPostAddService(string svc)
        {
            if(Input.Servicos != null)
            {
                var list = new List<string>();

                list.Add(svc);

                ServicosSelecionado = new SelectList(list, svc, svc, null); ;
            }

            return RedirectToPage();
        }

        public void OnPostRemoveService()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);

                    var t = Task.Run(() =>
                    {

                        var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(user.Id).FirstOrDefault();

                        var atold = _appServiceAtendimento.GetById(atendimemnto_ativio.Id);

                        atold.DataF = DateTime.Now;
                        atold.Setor = Input.Setor;
                        atold.Canal = Input.Canal;
                        atold.Servicos = Input.Servicos;
                        atold.Descricao = Input.Descricao;
                        atold.Status = "Finalizado";
                        atold.Ultima_Alteracao = DateTime.Now;

                        _appServiceAtendimento.Update(atold);

                    });

                    await t;

                    return RedirectToPage("./Index");

                }

                return Page();

            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }

    }
}
