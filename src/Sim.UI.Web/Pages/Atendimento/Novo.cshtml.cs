using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.Shared.Interface;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.SDE.Entity;

    public class NovoModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;

        public NovoModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSetor appServiceSetor)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceSetor = appServiceSetor;
            Input = new();
        }

        [BindProperty]
        public string GetCPF { get; set; }

        [BindProperty]
        public string GetCNPJ { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList Setores { get; set; }

        public SelectList Servicos { get; set; }

        public SelectList Canais { get; set; }

        private static string GetProtoloco()
        {
            return string.Format("{0}.{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        private async Task OnLoad()
        {
            var set = Task.Run(() => _appServiceSetor.List());
            await set;
            var serv = Task.Run(() => _appServiceServico.List());
            await serv;
            var can = Task.Run(() => _appServiceCanal.List());
            await can;

            if (set.Result != null)
            {
                Setores = new SelectList(set.Result, nameof(Setor.Id), nameof(Setor.Nome), null);
            }

            if (can.Result != null)
            {
                Canais = new SelectList(can.Result, nameof(Canal.Id), nameof(Canal.Nome), null);
            }

            if (serv.Result != null)
            {
                Servicos = new SelectList(serv.Result, nameof(Servico.Id), nameof(Servico.Nome), null);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input.Protocolo = GetProtoloco();
            Input.Data = DateTime.Now.Date;
            Input.Inicio = DateTime.Now;
            Input.Status = "ATIVO";
            await OnLoad();
            return Page();
        }

        public async Task<IActionResult> OnPostIncluirPessoaAsync()
        {

            var t = Task.Run(() => _appServicePessoa.ConsultaByCPF(GetCPF));
            await t;

            foreach(var p in t.Result)
            {
                Input.Pessoa = p;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostIncluirEmpresaAsync()
        {

            var t = Task.Run(() => _appServiceEmpresa.ConsultaByCNPJ(GetCNPJ));
            await t;

            foreach (var p in t.Result)
            {
                Input.Empresa = p;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            return RedirectToPage();
        }

    }
}
