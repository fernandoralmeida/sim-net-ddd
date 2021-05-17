using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using AutoMapper;
using System.Text;

namespace Sim.UI.Web.Pages.Empresa
{
    
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Sim.Service.CNPJ.WebService;
    using Sim.Service.CNPJ.Entity;


    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        public AddModel(IAppServiceEmpresa appServiceEmpresa, IMapper mapper, IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }


        [BindProperty(SupportsGet = true)]
        public VMEmpresa Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(string cnpj)
        {

            var rws = await _receitaWS.ConsultarCPNJAsync(cnpj);
            Input = _mapper.Map<VMEmpresa>(rws);

            foreach (var at in rws.AtividadePrincipal)
            {
                Input.CNAE_Principal = at.Code;
                Input.Atividade_Principal = at.Text;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var at in rws.AtividadesSecundarias)
            {
                sb.AppendLine(string.Format("{0} - {1}", at.Code, at.Text));
            }

            Input.Atividade_Secundarias = sb.ToString().Trim();

            var empresa = _mapper.Map<Empresa>(Input);

            _appServiceEmpresa.Add(empresa);
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                await LoadAsync(id);
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return RedirectToPage("./Index");
            }                     
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            { return Page(); }

            var t = Task.Run(() =>
            {                

                var empresa = _mapper.Map<Empresa>(Input);

                _appServiceEmpresa.Add(empresa);

            });

            await t;

            //StatusMessage = "Seu perfil foi atualizado";
            return RedirectToPage("./Index");
            //return Page();
        }
    }
}
