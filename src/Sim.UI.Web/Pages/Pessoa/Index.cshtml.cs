using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Pessoa
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;
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
            var pessoa = _pessoaApp.GetAll();

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
            var pessoa = _pessoaApp.ConsultaByCPF(Input.CPF);

            Input = new InputModel
            {
                ListaPessoas = pessoa
            };

            return Page();
        }

    }
}

