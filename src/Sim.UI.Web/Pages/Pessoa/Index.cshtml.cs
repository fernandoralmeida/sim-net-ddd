using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Pessoa
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServicePessoa _pessoaApp;

        public IndexModel(IAppServicePessoa appServicePessoa)
        {
            _pessoaApp = appServicePessoa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
         
            [DisplayName("Nome ou CPF")]            
            public string CPF { get; set; }

            [DisplayName("Nome ou CPF")]
            public string Nome { get; set; }

            public IEnumerable<Pessoa> ListaPessoas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }
        }

        private void Load()
        {
            var pessoa = _pessoaApp.List();

            Input = new InputModel
            {
                ListaPessoas = pessoa
            };
        }

        public IActionResult OnGet()
        {
            Load();
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pessoa = _pessoaApp.ConsultarPessoaByNameOrCPF(Input.CPF, Input.Nome);

                    Input = new InputModel
                    {
                        ListaPessoas = pessoa
                    };
                }

            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
            
            return Page();          
        }

    }
}

