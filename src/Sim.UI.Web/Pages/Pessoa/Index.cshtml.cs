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
    public class IndexModel : PageModel
    {
        private readonly IAppServicePessoa _pessoaApp;

        public IndexModel(IAppServicePessoa appServicePessoa)
        {
            _pessoaApp = appServicePessoa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public bool CpfValido = false;

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }
        public class InputModel
        {         
            [DisplayName("Informe o CPF")]            
            public string CPF { get; set; }
            public string Nome { get; set; }
            public string RouteCPF { get; set; }
            public IEnumerable<Pessoa> ListaPessoas { get; set; }
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
            Load();
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Input.CPF != null)
                    {
                        if (Functions.Validate.IsCpf(Input.CPF))
                        {
                            StatusMessage = "";
                            CpfValido = true;
                        }
                        else
                        {
                            StatusMessage = "Erro: CPF inválido!";
                            CpfValido = false;
                        }

                        var pessoa = _pessoaApp.ConsultaByCPF(Input.CPF);

                        Input = new InputModel
                        {
                            RouteCPF = new Functions.Mask().Remove(Input.CPF),
                            ListaPessoas = pessoa
                        };
                    }
                    else
                    {
                        Input = new InputModel
                        {
                            ListaPessoas = _pessoaApp.ConsultaByNome(Input.Nome)
                        };
                    }

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

