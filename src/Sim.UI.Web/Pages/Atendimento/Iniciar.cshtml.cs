using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.Shared.Interface;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.SDE.Entity;
    using Sim.Cross.Identity;
    using Functions;

    [Authorize]
    public class IniciarModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceContador _appServiceContador;

        public IniciarModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServiceContador appServiceContador)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceContador = appServiceContador;
        }

        [DisplayName("CPF")]
        [BindProperty(SupportsGet = true)]
        public string GetCPF { get; set; }

        [DisplayName("CNPJ")]
        [BindProperty(SupportsGet = true)]
        public string GetCNPJ { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private Task<string> GetProtoloco()
        {
            var t = _appServiceContador.GetProtocoloAsync(User.Identity.Name, "Atendimento");            
            return t;
        }
        private Task<Pessoa> GetPessoa(Guid id)
        {
            var t = Task.Run(() =>
            {
                return _appServicePessoa.GetById(id);
            });
            return t;
        }
        private Task<Empresas> GetEmpresa(string cnpj)
        {
            var t = Task.Run(() =>
            {
                var emp = _appServiceEmpresa.ConsultaByCNPJ(cnpj);

                if (!emp.Any())
                    StatusMessage = "Erro: Empresa não cadastrada!";
                else
                    StatusMessage = string.Empty;

                return emp.FirstOrDefault();
                
            });
            return t;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            Input = new();
            var at_ativo = Task.Run(() => _appServiceAtendimento.AtendimentoAtivo(User.Identity.Name));
       
            foreach (var at in await at_ativo)
            {
                StatusMessage = "Um atendimento encontra-se ativo, finalize antes de iniciar outro atendimento.";
                return RedirectToPage("/Atendimento/Novo/Index");
            }

            if (id != null)
            {
                Input.Pessoa = await GetPessoa((Guid)id);
                var emp = Task.Run(() => _appServiceEmpresa.ConsultaByRazaoSocial(Input.Pessoa.CPF.MaskRemove())
                .Where(s => s.Situacao_Cadastral != "BAIXADA"));                
                
                foreach(var e in await emp)
                    Input.Empresa = e;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostIncluirEmpresaAsync()
        {
            Input.Pessoa = await GetPessoa(Input.Pessoa.Id);
            Input.Empresa = await GetEmpresa(GetCNPJ);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoverEmpresaAsync()
        {
            Input.Empresa = null;
            Input.Pessoa = await GetPessoa(Input.Pessoa.Id);
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (Input.Pessoa == null)
            {
                return Page();
            }

            var atendimento = new Atendimento()
            {
                Protocolo = await GetProtoloco(),
                Data = DateTime.Now,
                Status = "Ativo",
                Ativo = true,
                Owner_AppUser_Id = User.Identity.Name
            };

            try
            {
                var t = Task.Run(() =>
                {                 
                    atendimento.Pessoa = _appServicePessoa.GetById(Input.Pessoa.Id);
                    atendimento.Empresa = _appServiceEmpresa.GetById(Input.Empresa.Id);
                    _appServiceAtendimento.Add(atendimento);
                });
                await t;
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return RedirectToPage("/Atendimento/Novo/Index");
        }       

    }
}
