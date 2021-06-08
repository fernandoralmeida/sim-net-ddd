using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.SDE.Interface;
    using Sim.Application.Shared.Interface;
    using Sim.Cross.Identity;
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.Shared.Entity;

    [Authorize]
    public class NovoModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
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

        [BindProperty(SupportsGet = true)]
        public string GetSetor { get; set; }
       
        public SelectList Setores { get; set; }

        public string GetServico { get; set; }
        public SelectList Servicos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string GetCanal { get;set; }
        public SelectList Canais { get; set; }

        public string MeusServicos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ServicosSelecionado { get; set; }

        private async Task OnLoad()
        {
            var set = Task.Run(() => _appServiceSetor.List());
            await set;        

            if (set.Result != null)
            {
                Setores = new SelectList(set.Result, nameof(Setor.Nome), nameof(Setor.Nome), null);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();

            //var user = await _userManager.GetUserAsync(User);

            var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(User.Identity.Name).FirstOrDefault();

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

        public JsonResult OnGetCanais()
        {            
            return new JsonResult(_appServiceCanal.GetByOwner(GetSetor));
        }

        public JsonResult OnGetServicos()
        {
            return new JsonResult(_appServiceServico.GetByOwner(GetSetor));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            try
            {
               
                    //var user = await _userManager.GetUserAsync(User);

                    var t = Task.Run(() =>
                    {
                        //var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(user.Id).FirstOrDefault();

                        var atold = _appServiceAtendimento.GetById(Input.Id);
                        atold.DataF = DateTime.Now;
                        atold.Setor = GetSetor;
                        atold.Canal = GetCanal;
                        atold.Servicos = Input.Servicos;
                        atold.Descricao = Input.Descricao;
                        atold.Status = "Finalizado";
                        atold.Ultima_Alteracao = DateTime.Now;
                        _appServiceAtendimento.Update(atold);

                    });

                    await t;

                    return RedirectToPage("./Index");

                

            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }

    }
}
