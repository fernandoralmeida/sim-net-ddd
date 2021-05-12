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

        [BindProperty(SupportsGet = true)]
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

            foreach (var q in rws.Qsa)
            {
                sb.AppendLine(string.Format("{0}: {1};", q.Qual, q.Nome));
            }

            Lista = sb.ToString();
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

                if (Lista != null)
                {
                    var qsa = new List<QSA>();

                    string[] st = Lista.ToString().Split(';');

                    foreach (var obj in st)
                    {
                        var s = obj.Trim();

                        if(s != string.Empty)
                        {
                            string[] i = s.ToString().Split(':');
                            qsa.Add(new QSA() { Qualificacao = i[0].Trim(), Nome = i[1].Trim() });
                        }

                    }

                    empresa.QSAs = qsa;
                }

                _appServiceEmpresa.Add(empresa);

            });

            await t;

            //StatusMessage = "Seu perfil foi atualizado";
            return RedirectToPage("./Index");
            //return Page();
        }
    }
}
