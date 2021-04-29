using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Application.Shared.Interface;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.SDE.Entity;
    using Sim.Cross.Identity;

    public class IniciarModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;

        public IniciarModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _userManager = userManager;
        }

        [BindProperty]
        public string GetCPF { get; set; }

        [BindProperty]
        public string GetCNPJ { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private static string GetProtoloco()
        {
            return string.Format("{0}{1}{2}", 
                DateTime.Now.Year, 
                DateTime.Now.Month, 
                DateTime.Now.Day);
        }

        private async Task OnLoad(Guid? id)
        {
            if (id != null)
            {
                var t = Task.Run(() => _appServicePessoa.GetById((Guid)id));
                await t;
                Input.Pessoa = t.Result;
            }
        }

        public async Task OnGetAsync(Guid? id)
        {
            Input = new();
            await OnLoad(id);
        }

        public async Task OnPostIncluirPessoaAsync()
        {
            var t = Task.Run(() => _appServicePessoa.ConsultaByCPF(GetCPF));
            await t;

            foreach (var p in t.Result)
            {
                Input.Pessoa = p;
            }
        }

        public async Task OnPostIncluirEmpresaAsync()
        {

            var t = Task.Run(() => _appServiceEmpresa.ConsultaByCNPJ(GetCNPJ));
            await t;

            foreach (var p in t.Result)
            {
                Input.Empresa = p;
            }
        }

        public async Task OnPostSaveAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            
            Input.Protocolo = Convert.ToInt32(GetProtoloco());
            Input.Data = DateTime.Now.Date;
            Input.Inicio = DateTime.Now;
            Input.Status = "ATIVO";
            Input.Ativo = true;
            Input.Owner_AppUser_Id = new Guid(user.Id);

            var t = Task.Run(() => _appServiceAtendimento.Add(_mapper.Map<Atendimento>(Input)));
            await t;
        }
        

    }
}
