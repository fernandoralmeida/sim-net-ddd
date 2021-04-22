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

        [BindProperty]
        [DisplayName("Quadro Administrativo")]
        public string Lista { get; set; }

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

            sb.Clear();
            //Lista = new List<string>();
            foreach(var q in rws.Qsa)
            {
                //Lista.Add(string.Format("{0} - {1};", q.Qual, q.Nome));
                sb.AppendLine(string.Format("{0}: {1};", q.Qual, q.Nome));
            }

            Lista = sb.ToString();
            //Lista = new SelectList(rws.Qsa, "Qual", "Nome");                     
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

            //await LoadAsync(new Functions.Mask().Remove(Input.CNPJ));
            //Input.QsaList = obj.Input.QsaList;

            var t = Task.Run(() =>
            {                

                var empresa = _mapper.Map<Empresa>(Input);

                var qsa = new List<QSA>();

                foreach (var obj in Lista)
                {
                    string[] st = obj.ToString().Split(':');

                    qsa.Add(new QSA() {Qualificacao = st[0].Trim(), Nome = st[1].Trim() });
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
