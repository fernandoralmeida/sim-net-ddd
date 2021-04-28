using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.Shared.Interface;
    public class NovoModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private static string _protocolo;
        public NovoModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
            Input = new();
            
        }

        public bool ExistePessoa()
        {
            if (Input.Pessoa == null)

                return false;
            else
                return true;
        }

        public bool ExisteEmpresa()
        {
            if (Input.Empresa == null)

                return false;
            else
                return true;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private static string GetProtoloco()
        {
            return string.Format("{0}.{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public void OnGet()
        {
            _protocolo = GetProtoloco();
            Input.Protocolo = _protocolo;
            Input.Data = DateTime.Now.Date;
            Input.Status = "ATIVO";
        }

        public async Task<IActionResult> OnPostIncluirPessoaAsync()
        {
            var t = Task.Run(() => {
                Input.Pessoa = new Domain.SDE.Entity.Pessoa() {                 
                    Nome= "Sim Teste Owner",
                    CPF = "000.000.000-00",
                    Tel_Movel="14 997177715",
                    Email="testesim@sim.com.br"
                };  
            });
            await t;
            ExistePessoa();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostIncluirEmpresaAsync()
        {
            var t = Task.Run(() => {
                Input.Empresa = new Domain.SDE.Entity.Empresa()
                {
                    Nome_Empresarial = "Sim Teste Owner",
                    CNPJ = "00.000.000/0000-00",
                    Telefone = "14 997177715",
                    Email = "testesim@sim.com.br"
                };
            });
            await t;
            ExisteEmpresa();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            return Page();
        }

    }
}
