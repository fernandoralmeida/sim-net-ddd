using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAppServiceQSA _appServiceQSA;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        public AddModel(IAppServiceEmpresa appServiceEmpresa, IAppServiceQSA appServiceQSA ,IMapper mapper, IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceQSA = appServiceQSA;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }

        [BindProperty]
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
            Input.QsaList = new List<QSA>();

            foreach (var qsa in rws.Qsa)
            {
                Input.QsaList.Add(new QSA() { Qualificacao = qsa.Qual, Nome = qsa.Nome });
            }
                      
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            
            await LoadAsync(id);            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            { return Page(); }

            await LoadAsync(new Functions.Mask().Remove(Input.CNPJ));

            var t = Task.Run(() =>
            {

                var empresa = _mapper.Map<Empresa>(Input);

                var qsa = new List<QSA>();

                foreach (var obj in Input.QsaList)
                {
                    qsa.Add(new QSA() { Nome = obj.Nome, Qualificacao = obj.Qualificacao });
                }

                empresa.QSAs = qsa;

                _appServiceEmpresa.Add(empresa);

            });

            await t;



            //StatusMessage = "Seu perfil foi atualizado";
            //return RedirectToPage("./Index");
            return Page();
        }
    }
}
