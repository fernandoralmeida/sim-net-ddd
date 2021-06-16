using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;
    using Sim.Application.Interface;


    [Authorize(Roles = "Administrador,M_Jucesp")]
    public class Jucesp_consulta_atividadeModel : PageModel
    {
        private readonly ICNPJBase<BaseJucesp> _empresaApp;
        private readonly IBase<CNAE> _cnaes;

        public Jucesp_consulta_atividadeModel(ICNPJBase<BaseJucesp> appServiceEmpresa,
            IBase<CNAE> cnaes)
        {
            _empresaApp = appServiceEmpresa;
            _cnaes = cnaes;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList ListaCnaes { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [Required]
            [DisplayName("Atividade")]
            public string Atividade { get; set; }

            public IEnumerable<BaseJucesp> ListaEmpresas { get; set; }

            [TempData]
            public string StatusMessage { get; set; }

            [Required]
            public string Municipio { get; set; }
        }

        private async Task LoadCnaes()
        {
            var t = await _cnaes.ListAll();

            if (t != null)
            {
                ListaCnaes = new SelectList(t, nameof(CNAE.Codigo), nameof(CNAE.Descricao), null);
            }
        }

        public async Task OnGetAsync()
        {
            await LoadCnaes();
            Input = new() { Municipio = "Jau" };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await LoadCnaes();

                    var emp = Task.Run(() => _empresaApp.ListByAtividadeAsync(Input.Atividade, Input.Municipio));

                    await emp;

                    Input = new InputModel
                    {
                        ListaEmpresas = emp.Result,
                    };
                }

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }
    }
}
