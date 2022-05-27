using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda
{
    using Sim.Application.SDE;
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex Input { get; set; }

        public class InputModelIndex
        {
            [DisplayName("Nome Evento")]
            public string Evento { get; set; }
            public int Ano { get; set; }
            public IEnumerable<InputModelEvento> ListaEventos { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceEvento appServiceEvento,
            IAppServiceInscricao appServiceInscricao,
            IMapper mapper)
        {
            _mapper = mapper;
            _appServiceEvento = appServiceEvento;
            _appServiceInscricao = appServiceInscricao;
        }

        public async Task OnGetAsync()
        {
            Input.Ano = DateTime.Now.Year;
            var t = await _appServiceEvento.EventosAtivos();
            Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>(t);
        }

        public async Task OnPostEventAsync()
        {
            var t = Task.Run(() =>
            {
                Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>
                (_appServiceEvento.GetByNome(Input.Evento));
            });
            await t;
        }
        public async Task OnPostEventAvailAsync()
        {
            var t = Task.Run(() =>
            {
                Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>
                (_appServiceEvento.EventosAtivos().Result);
            });
            await t;
        }

        public async Task OnPostEventOldAsync()
        {
            var t = Task.Run(() =>
            {
                Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>
                (_appServiceEvento.EventosPassados().Result);
            });
            await t;
        }

        private int QuantosDiasFaltam(DateTime dataalvo)
        {
            return (int)dataalvo.Subtract(DateTime.Today).TotalDays;
        }
    }
}
