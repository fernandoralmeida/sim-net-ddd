using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.Shared.Interface;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.SDE.Entity;
    using Sim.Cross.Identity;

    public class IniciarModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceContador _appServiceContador;
        private readonly IMapper _mapper;

        public IniciarModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServiceContador appServiceContador,
            IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _userManager = userManager;
            _appServiceContador = appServiceContador;
        }

        [BindProperty]
        public string GetCPF { get; set; }

        [BindProperty]
        public string GetCNPJ { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private string GetProtoloco()
        {
            var user = _userManager.GetUserAsync(User);
            user.Wait();
            var t = _appServiceContador.GetProtocoloAsync(user.Result.Id, "Atendimento");
            t.Wait();
            return t.Result.ToString();
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            Input = new();
            var user = await _userManager.GetUserAsync(User);
            var at_ativo = Task.Run(() => _appServiceAtendimento.AtendimentoAtivo(user.Id));
            at_ativo.Wait();

            foreach (var at in at_ativo.Result)
            {
                StatusMessage = "Um atendimento encontra-se ativo. Finalize antes de iniciar outro atendimento.";
                return RedirectToPage("./Novo");
            }

            if (id != null)
            {
                var t = Task.Run(() => _appServicePessoa.GetById((Guid)id));
                await t;
                Input.Pessoa = t.Result;
            }

            return Page();
        }

        public async Task OnPostIncluirPessoaAsync()
        {
            var t = Task.Run(() => _appServicePessoa.ConsultaByCPF(GetCPF));
            await t;

            foreach (var p in t.Result)
            {
                Input.Pessoa = p;
            }
        }

        public async Task OnPostIncluirEmpresaAsync()
        {

            var t = Task.Run(() => _appServiceEmpresa.ConsultaByCNPJ(GetCNPJ));
            await t;

            foreach (var p in t.Result)
            {
                Input.Empresa = p;
            }
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            var user = await _userManager.GetUserAsync(User);          

                Input.Protocolo = GetProtoloco();
                Input.Data = DateTime.Now.Date;
                Input.Status = "ATIVO";
                Input.Ativo = true;
                Input.Owner_AppUser_Id = user.Id;

            var atendimento = new Atendimento()
            {
                Protocolo = Input.Protocolo,
                Data = Input.Data,
                Status = Input.Status,
                Ativo = Input.Ativo,
                Owner_AppUser_Id = Input.Owner_AppUser_Id
            };

            var pessoa = _appServicePessoa.GetById(Input.Pessoa.Id);
            
            if(pessoa != null)
                atendimento.Pessoa = pessoa;

            if (Input.Empresa != null)
            {

                var empresa = _appServiceEmpresa.GetById(Input.Empresa.Id);

                if (empresa != null)
                    atendimento.Empresa = empresa;
            }

            _appServiceAtendimento.Add(atendimento); 

            StatusMessage = Input.Pessoa.Data_Nascimento.ToString();

            return Page();
        }        

    }
}
