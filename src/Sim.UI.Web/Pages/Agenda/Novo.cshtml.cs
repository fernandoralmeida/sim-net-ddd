using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda
{
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;
    public class NovoModel : PageModel
    {
        private readonly IAppServiceTipo _appServiceTipo;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IMapper _mapper;

        public NovoModel(IAppServiceEvento appServiceEvento,
            IAppServiceTipo appServiceTipo,
            IAppServiceSetor appServiceSetor,
            IMapper mapper)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceTipo = appServiceTipo;
            _appServiceSetor = appServiceSetor;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList TipoEventos { get; set; }
        public SelectList Setores { get; set; }

        public async Task OnGet()
        {
            var t = Task.Run(() => _appServiceTipo.List());
            await t;

            var s = Task.Run(() => _appServiceSetor.List());
            await s;

            if (t.Result != null)
            {
                TipoEventos = new SelectList(t.Result, nameof(Tipo.Nome), nameof(Tipo.Nome), null);
            }

            if (s.Result != null)
            {
                Setores = new SelectList(s.Result, nameof(Setor.Nome), nameof(Setor.Nome), null);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if(!ModelState.IsValid)
                { return Page(); }

                var cod = Task.Run(()=> _appServiceEvento.LastCodigo());
                await cod;

                if (cod.Result < 1)
                    Input.Codigo = 210001;
                else
                    Input.Codigo = cod.Result + 1;

                Input.Estado = "Ativo";
                Input.Ativo = true;

                var t = Task.Run(() => _appServiceEvento.Add(_mapper.Map<Evento>(Input)));

                await t;
                
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }
    }
}
