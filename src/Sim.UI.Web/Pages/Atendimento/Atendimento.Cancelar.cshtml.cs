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
    public class AtendimentoCancelarModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;

        public AtendimentoCancelarModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSetor appServiceSetor)
        {
            _appServiceAtendimento = appServiceAtendimento;
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
        public string GetCanal { get; set; }
        public SelectList Canais { get; set; }

        public string MeusServicos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ServicosSelecionado { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            var t = Task.Run(()=> _appServiceAtendimento.GetAtendimento(id));

            var atendimemnto_ativio = await t;

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

            return Page();
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

                    var t = Task.Run(() =>
                    {

                        var atold = _appServiceAtendimento.GetById(Input.Id);

                        var sebrae = new RaeSebrae() { Id = new Guid(), RAE = Input.Sebrae.RAE };

                        atold.DataF = DateTime.Now;
                        atold.Setor = GetSetor;
                        atold.Canal = GetCanal;
                        atold.Servicos = Input.Servicos;
                        atold.Descricao = Input.Descricao;
                        atold.Status = "Cancelado";
                        atold.Ultima_Alteracao = DateTime.Now;
                        atold.Sebrae = sebrae;
                        _appServiceAtendimento.Update(atold);

                    });

                    await t;

                    return RedirectToPage("./Consulta.Cancelados");

                }

                return Page();

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }
    }
}
