using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda
{
    using Sim.Application.SDE;
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;


    public class IndexModel : PageModel
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

        public IndexModel(IAppServiceEvento appServiceEvento,
            IMapper mapper)
        {
            _mapper = mapper;
            _appServiceEvento = appServiceEvento;
        }

        public async Task OnGet()
        {
            var t = Task.Run(() => _appServiceEvento.List());
            await t;
            Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>(t.Result);
        }

        public async Task OnPostAsync()
        {
            var t = Task.Run(()=> _appServiceEvento.GetByNome(Input.Evento));
            await t;
            Input.ListaEventos = _mapper.Map<IEnumerable<InputModelEvento>>(t.Result);
        }
    }
}
