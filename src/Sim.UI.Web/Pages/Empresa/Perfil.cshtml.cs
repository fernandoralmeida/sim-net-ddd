using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Sim.Service.CNPJ.WebService;
    using Sim.Service.CNPJ.Entity;

    [Authorize]
    public class PerfilModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceQSA _appServiceQSA;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;
        

        public PerfilModel(IAppServiceEmpresa appServiceEmpresa, IAppServiceQSA appServiceQSA, IMapper mapper, IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceQSA = appServiceQSA;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }

        [BindProperty(SupportsGet = true)]
        public VMEmpresa Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(Guid id)
        {
            var t = Task.Run(() => {

                var emp = _appServiceEmpresa.ListEmpresasQsa(id).FirstOrDefault();
                
                Input = _mapper.Map<VMEmpresa>(emp);

                List<QSA> list = new List<QSA>();

                if (emp.QSAs != null)
                    foreach (var obj in emp.QSAs)
                    {
                        
                        list.Add(new QSA() { Nome = obj.Nome, Qualificacao = obj.Qualificacao });
                    }

                Input.QsaList = list;
            });

            await t;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            await LoadAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostRWSAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
                        
            var rws = await _receitaWS.ConsultarCPNJAsync(new Functions.Mask().Remove(Input.CNPJ));
            
            Input = new();
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

            List<QSA> list = new List<QSA>();

            if (rws.Qsa != null)
                foreach (var obj in rws.Qsa)
                {

                    list.Add(new QSA() { Nome = obj.Nome, Qualificacao = obj.Qual });
                }

            Input.QsaList = list;

            await TryUpdateModelAsync<VMEmpresa>(Input, "", s => s.Nome_Empresarial, s => s.Nome_Fantasia);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var t = Task.Run(() =>
            {

                var empresa = _mapper.Map<Empresa>(Input);

                _appServiceEmpresa.Update(empresa);

            });

            await t;

            return RedirectToPage();
        }
    }
}
