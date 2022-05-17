using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Sim.UI.Web.Pages.Pat.Edit
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Functions;

    public class IndexModel : PageModel
    {
        
        private readonly IAppServiceEmpregos _appServiceEmpregos;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public IndexModel(IAppServiceEmpregos appServiceEmpregos)
        {
            _appServiceEmpregos = appServiceEmpregos;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Input = new();

            var t = await _appServiceEmpregos.GetByIdAsync(id);
            
            foreach (var e in t)
            {
                Input.Id = e.Id;
                Input.Empresa = e.Empresa;
                Input.Ocupacao = e.Ocupacao;
                Input.Vagas = e.Vagas;
                Input.Salario = e.Salario;
                Input.Pagamento = e.Pagamento;
                Input.Experiencia = e.Experiencia;
                Input.Data = e.Data;
            }       
            return Page();
        }

        public async Task<IActionResult> OnPostExclrAsync()
        {

            var t = Task.Run(() =>
            {
                var emprego = new Empregos()
                {
                    Id = Input.Id,
                    Data = Input.Data,
                    Experiencia = Input.Experiencia,
                    Vagas = Input.Vagas,
                    Ocupacao = Input.Ocupacao,
                    Pagamento = Input.Pagamento,
                    Salario = Input.Salario
                };
                _appServiceEmpregos.Remove(emprego);
            });

            await t;

            return RedirectToPage("/Pat/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { return Page(); }

            var t = Task.Run(() =>
            {
                //var oldemprego = _appServiceEmpregos.GetById(Input.Id);

                var emprego = new Empregos()
                {   
                    Id = Input.Id,
                    Data = Input.Data,
                    Experiencia = Input.Experiencia,
                    Vagas = Input.Vagas,
                    Ocupacao = Input.Ocupacao,
                    Pagamento = Input.Pagamento,
                    Salario = Input.Salario
                };

                _appServiceEmpregos.Update(emprego);

            });
            await t;

            return RedirectToPage("/Pat/Index");
        }
    }
}