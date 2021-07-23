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

    [Authorize]
    public class Inscricao_RemoveModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IMapper _mapper;
        public Inscricao_RemoveModel(IAppServiceEvento appServiceEvento,
            IAppServiceInscricao appServiceInscricao,
            IMapper mapper)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceInscricao = appServiceInscricao;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task OnGetAsync(int id)
        {

            var t = Task.Run(() => _appServiceEvento.GetByCodigo(id));
            await t;

            Input = _mapper.Map<InputModelEvento>(t.Result);
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid id, int ide)
        {
            var inscrito = Task.Run(() => _appServiceInscricao.GetById(id));
            await inscrito;

            var t = Task.Run(() => _appServiceInscricao.Remove(inscrito.Result));
            await t;

            return RedirectToPage("./Inscricao.Remove", new { id = ide });
        }

        public async Task<JsonResult> OnGetDetalheInscrito(string id)
        {
            var detalhe = await _appServiceInscricao.GetInscrito(new Guid(id));

            return new JsonResult(detalhe);
        }
    }
}
