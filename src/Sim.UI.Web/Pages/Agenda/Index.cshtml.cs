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
            public IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)> ListaEventosMes { get; set; }
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
            Input.ListaEventosMes = await _appServiceEvento.ListarEventosPorMes(await _appServiceEvento.EventosAtivos(Input.Ano));
        }

        public async Task OnPostEventAsync()
        {
            Input.ListaEventosMes = await _appServiceEvento.ListarEventosPorMes(_appServiceEvento.GetByNome(Input.Evento, Input.Ano));
        }
        public async Task OnPostAvailableAsync()
        {
            Input.ListaEventosMes = await _appServiceEvento.ListarEventosPorMes(await _appServiceEvento.EventosAtivos(Input.Ano));
        }

        public async Task OnPostFinalizedAsync()
        {

            Input.ListaEventosMes = await _appServiceEvento.ListarEventosPorMes(await _appServiceEvento.EventosFinalizados(Input.Ano));
        }

        public async Task OnPostCanceledAsync()
        {

            Input.ListaEventosMes = await _appServiceEvento.ListarEventosPorMes(await _appServiceEvento.EventosCancelados(Input.Ano));
        }

        private int QuantosDiasFaltam(DateTime dataalvo)
        {
            return (int)dataalvo.Subtract(DateTime.Today).TotalDays;
        }
    }
}
