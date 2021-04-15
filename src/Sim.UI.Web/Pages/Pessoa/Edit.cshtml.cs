using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Pessoa
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IAppServicePessoa _pessoa;
        private readonly IMapper _mapper;

        public EditModel(IAppServicePessoa pessoa, IMapper mapper)
        {
            _pessoa = pessoa;
            _mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public int Id { get; set; }

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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t = Task.Run(
                () => _pessoa.GetById((int)id));

            await t;


            Input = _mapper.Map<InputModel>(t.Result);

            if (Input.Deficiencia != null)
            {

                if (t.Result.Deficiencia.Contains("Física"))
                    Input.Fisica = true;

                if (t.Result.Deficiencia.Contains("Visual"))
                    Input.Visual = true;

                if (t.Result.Deficiencia.Contains("Auditiva"))
                    Input.Auditiva = true;

                if (t.Result.Deficiencia.Contains("Intelectual"))
                    Input.Intelectual = true;
            }


            if (Input == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

                    var task = Task.Run(() => {

                        var pessoa = _mapper.Map<Pessoa>(Input);

                        pessoa.Deficiencia = string.Empty;

                        if (Input.Fisica)
                            pessoa.Deficiencia += "Física;";

                        if (Input.Visual)
                            pessoa.Deficiencia += "Visual;";

                        if (Input.Auditiva)
                            pessoa.Deficiencia += "Auditiva;";

                        if (Input.Intelectual)
                            pessoa.Deficiencia += "Intelectual;";

                        _pessoa.Update(pessoa);

                    });

                    await task;

                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }

        private Pessoa PessoaExists(int id)
        {
            return _pessoa.GetById(id);
        }

    }
}
