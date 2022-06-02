using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda.Eventos.Edit
{
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceTipo _appServiceTipo;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceParceiro _appServiceParceiro;
        private readonly IMapper _mapper;

        public IndexModel(IAppServiceEvento appServiceEvento,
            IAppServiceTipo appServiceTipo,
            IAppServiceSetor appServiceSetor,
            IAppServiceParceiro appServiceParceiro,
            IMapper mapper)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceTipo = appServiceTipo;
            _appServiceSetor = appServiceSetor;
            _appServiceParceiro = appServiceParceiro;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList TipoEventos { get; set; }
        public SelectList Setores { get; set; }

        public SelectList Parceiros { get; set; }
        public SelectList Situacoes { get; set; }

        private async Task Onload()
        {
            var t = Task.Run(() => _appServiceTipo.List());
            await t;

            var s = Task.Run(() => _appServiceSetor.List());
            await s;

            var p = Task.Run(() => _appServiceParceiro.List());

            if (t.Result != null)
            {
                TipoEventos = new SelectList(t.Result, nameof(Tipo.Nome), nameof(Tipo.Nome), null);
            }

            if (s.Result != null)
            {
                Setores = new SelectList(s.Result, nameof(Setor.Nome), nameof(Setor.Nome), null);
            }

            if (p.Result != null)
            {
                Parceiros = new SelectList(p.Result, nameof(Parceiro.Nome), nameof(Parceiro.Nome), null);
            }
            Situacoes = new SelectList(Enum.GetNames(typeof(Evento.ESituacao)));
        }

        public async Task OnGet(Guid id)
        {
            await Onload();
            var evento = Task.Run(() =>
            {

                Input = _mapper.Map<InputModelEvento>(_appServiceEvento.GetById(id));

            });
            await evento;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StatusMessage = "Verifique o preenchimento correto do formulário!";
                    await Onload();
                    return Page();
                }

                var t = Task.Run(() => _appServiceEvento.Update(_mapper.Map<Evento>(Input)));

                await t;

                return RedirectToPage("/Agenda/Index");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }
    }
}
