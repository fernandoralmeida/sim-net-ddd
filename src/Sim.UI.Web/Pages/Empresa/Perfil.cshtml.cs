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

        [BindProperty]
        public VMEmpresa Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(Guid cnpj)
        {
            var emp = Task.Run(() => _appServiceEmpresa.GetById(cnpj));
            await emp;

            Input = _mapper.Map<VMEmpresa>(emp.Result);

            if (emp.Result.QSAs != null)
                foreach (var obj in emp.Result.QSAs)
                {
                    //Input.QsaList.Add(new QSA() { Nome = obj.Nome, Qualificacao = obj.Qualificacao });
                }
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            await LoadAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                var t = Task.Run(() => _appServiceEmpresa.Add(_mapper.Map<Empresa>(Input)));
                await t;

                var q = Task.Run(() => _appServiceEmpresa.ConsultaByCNPJ(Input.CNPJ));
                await q;

                var rws = await _receitaWS.ConsultarCPNJAsync(new Functions.Mask().Remove(Input.CNPJ));

                Input.QsaList = new List<QSA>();

                foreach (var qsa in rws.Qsa)
                {
                    Input.QsaList.Add(new QSA() { Qualificacao = qsa.Qual, Nome = qsa.Nome });
                }

                if (Input.QsaList != null)
                {

                    foreach (var qsa in Input.QsaList)
                    {

                        var qt = Task.Run(() =>
                        {

                            _appServiceQSA.Add(new QSA()
                            {
                                //Empresa_Id = q.Id,
                                Nome = qsa.Nome,
                                Qualificacao = qsa.Qualificacao
                            });

                        });
                        await qt;

                        StatusMessage = qsa.Nome;
                    }
                }

            }

            //StatusMessage = "Seu perfil foi atualizado";
            //return RedirectToPage("./Index");
            return Page();
        }
    }
}
