using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda
{
    using Sim.Application.Shared.Interface;
    using System.ComponentModel;

    [Authorize]
    public class Evento_list_passadosModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex Input { get; set; }

        public class InputModelIndex
        {
            [DisplayName("Nome")]
            public string Evento { get; set; }
            public IEnumerable<InputModelEvento> ListaEventos { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public Evento_list_passadosModel(IAppServiceEvento appServiceEvento,
            IMapper mapper)
        {
            _mapper = mapper;
            _appServiceEvento = appServiceEvento;
        }

        public async Task OnGetAsync()
        {
            //var t = Task.Run(() => _appServiceEvento.ListAll());
            var t = await _appServiceEvento.EventosPassados();
            Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>(t);
        }

        public async Task OnPostAsync()
        {
            var t = await _appServiceEvento.EventosPassados();
            Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>(t);
        }
    }
}
