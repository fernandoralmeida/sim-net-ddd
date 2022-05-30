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
    using System.ComponentModel.DataAnnotations;
    using global::AutoMapper;
    

    [Authorize]
    public class NovoModel : PageModel
    {
        private readonly IAppServicePessoa _pessoa;
        private readonly IMapper _mapper;
        public NovoModel(IAppServicePessoa appServicePessoa, IMapper mapper)
        {
            _pessoa = appServicePessoa;
            _mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        //[BindProperty]
        //public Pessoa Input { get; set; }
        public string ReturnUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel 
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Pessoa_Id { get; set; }

            // Pessoal
            [Required(ErrorMessage = "Preencha campo Nome")]
            public string Nome { get; set; }

            [DisplayName("Nome Social")]
            public string Nome_Social { get; set; }

            [Required(ErrorMessage = "Preencha data de nascimento")]
            [DisplayName("Data de Nascimento")]
            [DataType(DataType.Date)]
            public DateTime? Data_Nascimento { get; set; }

            [Required(ErrorMessage = "Preencha o campo CPF")]
            public string CPF { get; set; }

            public string RG { get; set; }

            [DisplayName("Emissor")]
            public string RG_Emissor { get; set; }

            [DisplayName("UF")]
            public string RG_Emissor_UF { get; set; }

            [Required(ErrorMessage = "Preencha o campo gênero")]
            [DisplayName("Gênero")]
            public string Genero { get; set; }

            public string Deficiencia { get; set; }

            [DisplayName("Física")]
            public bool Fisica { get; set; }

            [DisplayName("Visual")]
            public bool Visual { get; set; }

            [DisplayName("Auditiva")]
            public bool Auditiva { get; set; }

            [DisplayName("Intelectual")]
            public bool Intelectual { get; set; }

            //Correspondencia
            public string CEP { get; set; }
            public string Logradouro { get; set; }
            public string Numero { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string UF { get; set; }

            //Contato
            //public List<KeyValuePair<string, string>> Telefones { get; set; }
            [DisplayName("Telefone Móvel")]
            public string Tel_Movel { get; set; }

            [DisplayName("Telefone Fixo")]
            public string Tel_Fixo { get; set; }

            [DisplayName("E-mail")]
            public string Email { get; set; }

            //Informacao Cadastro
            [DisplayName("Data de Cadastro")]
            public DateTime? Data_Cadastro { get; set; }

            [DisplayName("Ultima Alteração")]
            public DateTime? Ultima_Alteracao { get; set; }

            [DisplayName("Cadastro")]
            public bool Ativo { get; set; }
        }

        public IActionResult OnGet(string id)
        {
            
            Input = new InputModel
            {
                CPF = id,
                Data_Cadastro = DateTime.Now,
                Ultima_Alteracao = DateTime.Now,
                Ativo = true                
            };

            StatusMessage = string.Empty;

            if(!Functions.Validate.IsCpf(id))
            {
                StatusMessage = "Erro: CPF inválido!";
                return RedirectToPage("/Pessoa/Index");
            }
            else                
                return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var pessoa = _mapper.Map<Pessoa>(Input);

                if (Input.Fisica)
                    pessoa.Deficiencia += "Física;";

                if (Input.Visual)
                    pessoa.Deficiencia += "Visual;";

                if (Input.Auditiva)
                    pessoa.Deficiencia += "Auditiva;";

                if (Input.Intelectual)
                    pessoa.Deficiencia += "Intelectual;";

                _pessoa.Add(pessoa);

                return RedirectToPage("/Pessoa/Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + "Verifique se o Cliente já está cadastrado ou contate o suporte! - " + ex.Message;
                return Page();
            }


        }


    }
}
