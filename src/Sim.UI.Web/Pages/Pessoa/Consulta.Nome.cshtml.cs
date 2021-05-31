using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Pessoa
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;

    [Authorize]
    public class ConsultaNomeModel : PageModel
    {
        private readonly IAppServicePessoa _pessoaApp;

        public ConsultaNomeModel(IAppServicePessoa appServicePessoa)
        {
            _pessoaApp = appServicePessoa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Required]
            [DisplayName("Nome ou CPF")]
            public string CPF { get; set; }

            public string Nome { get; set; }

            public IEnumerable<Pessoa> ListaPessoas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }
        }

        private void Load()
        {
            var pessoa = _pessoaApp.Top10();

            Input = new InputModel
            {
                ListaPessoas = pessoa
            };
        }

        public IActionResult OnGet()
        {
            //Load();
            Input = new InputModel
            {
                ListaPessoas = null
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            var t = Task.Run(() =>
            {

                var pessoa = _pessoaApp.ConsultaByNome(Input.Nome);

                Input.ListaPessoas = pessoa;
            });

            await t;

            return Page();
        }
    }
}
