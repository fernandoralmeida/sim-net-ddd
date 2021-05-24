using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda
{
    using Sim.Application.Shared.Interface;
    using System.ComponentModel;

    public class Evento_ListModel : PageModel
    {
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex Input { get; set; }

        public class InputModelIndex
        {
            [DisplayName("Nome")]
            public string GetCPF { get; set; }
            public IEnumerable<InputModelInscricao> ListaInscricoes { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public Evento_ListModel(IAppServiceInscricao appServiceEvento,
            IMapper mapper)
        {
            _mapper = mapper;
            _appServiceInscricao = appServiceEvento;
        }

        public void OnGet()
        {      }

        public async Task OnPostAsync()
        {
            var t = Task.Run(() => _appServiceInscricao.GetByParticipante(Input.GetCPF));
            await t;
            Input.ListaInscricoes = _mapper.Map<IEnumerable<InputModelInscricao>>(t.Result);
        }
    }
}
