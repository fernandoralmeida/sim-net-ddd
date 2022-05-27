using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda.Inscricoes.Lista
{
    using Sim.Application.Shared.Interface;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IMapper _mapper;
        public IndexModel(IAppServiceEvento appServiceEvento, IMapper mapper)
        {
            _appServiceEvento = appServiceEvento;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task OnGetAsync(int id)
        {

            var t = Task.Run(() => _appServiceEvento.GetByCodigo_Participantes((int)id));
            await t;

            Input = _mapper.Map<InputModelEvento>(t.Result);
        }
    }
}
