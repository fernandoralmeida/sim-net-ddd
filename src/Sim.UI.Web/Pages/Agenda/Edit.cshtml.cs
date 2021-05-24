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
    public class EditModel : PageModel
    {
        private readonly IAppServiceTipo _appServiceTipo;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IMapper _mapper;

        public EditModel(IAppServiceEvento appServiceEvento,
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

        public async Task OnGet(Guid id)
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

            var evento = Task.Run(() => _appServiceEvento.GetById(id));
            await evento;

            Input = _mapper.Map<InputModelEvento>(evento.Result);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                { return Page(); }
                               
                var t = Task.Run(() => _appServiceEvento.Update(_mapper.Map<Evento>(Input)));

                await t;

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }
    }
}

