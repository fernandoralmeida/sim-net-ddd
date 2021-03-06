using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.SDE.Interface;
    using Sim.Application.Shared.Interface;
    using Sim.Cross.Identity;
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.Shared.Entity;


    [Authorize]
    public class EditModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;

        public EditModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSetor appServiceSetor)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceSetor = appServiceSetor;
            //_userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [Required(ErrorMessage = "Selecione o setor do atendimento!")]
        [BindProperty(SupportsGet = true)]
        public string GetSetor { get; set; }

        public SelectList Setores { get; set; }
        public SelectList Canais { get; set; }
        public SelectList Servicos { get; set; }

        public string GetServico { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ServicosSelecionados { get; set; }

        private async Task OnLoad()
        {
            var set = Task.Run(() => _appServiceSetor.List());
            await set;

            var lst = new List<Setor>();
            foreach (var s in set.Result)
            {
                if (s.Nome != "Geral")
                    lst.Add(new Setor() { Nome = s.Nome, Secretaria = s.Secretaria, Id = s.Id, Ativo = s.Ativo, Canais = s.Canais, Servicos = s.Servicos });
            }

            if (lst != null)
            {
                Setores = new SelectList(lst, nameof(Setor.Nome), nameof(Setor.Nome), null);
            }
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            await OnLoad();

            //var user = await _userManager.GetUserAsync(User);

            var atendimemnto_ativio = _appServiceAtendimento.GetAtendimento((Guid)id);

            if(atendimemnto_ativio.Owner_AppUser_Id != User.Identity.Name)
            {
                StatusMessage = "Erro : Atendimento pertence a outro atendente!";
                return RedirectToPage("./Index");
            }

            if (atendimemnto_ativio == null)
            {
                StatusMessage = "Erro inesperado, tente novamente!";
                return RedirectToPage("./Index");
            }

            Input = new()
            {
                Id = atendimemnto_ativio.Id,
                Protocolo = atendimemnto_ativio.Protocolo,
                Data = atendimemnto_ativio.Data,
                DataF = atendimemnto_ativio.DataF,
                Setor = atendimemnto_ativio.Setor,
                Canal = atendimemnto_ativio.Canal,
                Servicos = atendimemnto_ativio.Servicos,
                Descricao = atendimemnto_ativio.Descricao,
                Status = atendimemnto_ativio.Status,
                Ultima_Alteracao = atendimemnto_ativio.Ultima_Alteracao,
                Ativo = atendimemnto_ativio.Ativo,
                Owner_AppUser_Id = atendimemnto_ativio.Owner_AppUser_Id,
                Pessoa = atendimemnto_ativio.Pessoa,
                Empresa = atendimemnto_ativio.Empresa                
            };

            var canal = Task.Run(() => _appServiceCanal.GetByOwner(Input.Setor));
            await canal;

            if (canal.Result != null)
            {
                Canais = new SelectList(canal.Result, nameof(Canal.Nome), nameof(Canal.Nome), null);
            }

            var serv = Task.Run(() => _appServiceServico.GetByOwner(Input.Setor));
            await serv;

            if (serv.Result != null)
            {
                Servicos = new SelectList(serv.Result, nameof(Servico.Nome), nameof(Servico.Nome), null);
            }
                        
            Input.Canal = atendimemnto_ativio.Canal;            
            
            Input.Servicos = atendimemnto_ativio.Servicos;
            ServicosSelecionados = atendimemnto_ativio.Servicos; 

            return Page();
        }

        public JsonResult OnGetCanais()
        {
            return new JsonResult(_appServiceCanal.GetByOwner(GetSetor));
        }

        public JsonResult OnGetServicos()
        {
            return new JsonResult(_appServiceServico.GetByOwner(GetSetor));
        }

        public async Task<IActionResult> OnPostAlterarAsync(Guid id)
        {

            try
            {

                //var user = await _userManager.GetUserAsync(User);

                var t = Task.Run(() =>
                {
                    //var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(user.Id).FirstOrDefault();

                    var atold = _appServiceAtendimento.GetById(id);
                    atold.Setor = Input.Setor; //GetSetor;
                    atold.Canal = Input.Canal;  //GetCanal;

                    if(Input.Servicos!=null || Input.Servicos!=string.Empty)
                        atold.Servicos = ServicosSelecionados; //MeusServicos;
                    else
                        atold.Servicos = Input.Servicos; //MeusServicos;

                    atold.Descricao = Input.Descricao;
                    atold.Status = "Finalizado";
                    atold.Ultima_Alteracao = DateTime.Now;
                    _appServiceAtendimento.Update(atold);

                });

                await t;

                return RedirectToPage("./Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }

        public async Task<IActionResult> OnPostExcluirAsync(Guid id)
        {

            try
            {

                var t = Task.Run(() =>
                {
                    //var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(user.Id).FirstOrDefault();

                    var atold = _appServiceAtendimento.GetById(id);
                    _appServiceAtendimento.Remove(atold);

                });

                await t;

                return RedirectToPage("./Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }
    }
}
