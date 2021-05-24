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

    public class Inscritos_ListModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IMapper _mapper;
        public Inscritos_ListModel(IAppServiceEvento appServiceEvento, IMapper mapper)
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

            var t = Task.Run(() => _appServiceEvento.GetByCodigo((int)id));
            await t;

            Input = _mapper.Map<InputModelEvento>(t.Result);
        }
    }
}
