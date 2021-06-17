using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Sim.UI.Web.Pages.Agenda
{
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.SDE.Entity;
    using Sim.Application.Shared.Interface;
    using Sim.Application.SDE.Interface;
    using Sim.Cross.Identity;

    [Authorize]
    public class Inscricao_EventoModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        
        public Inscricao_EventoModel(IAppServiceInscricao appServiceInscricao,
            IAppServiceEvento appServiceEvento,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServicePessoa appServicePessoa)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceInscricao = appServiceInscricao;
            _appServiceEmpresa = appServiceEmpresa;
            _appServicePessoa = appServicePessoa;
            //_userManager = userManager;
        }

        [BindProperty]
        public string GetCPF { get; set; }

        [BindProperty]
        public string GetCNPJ { get; set; }
        [BindProperty]
        public int GetNumeroEvento { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelInscricao Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public ICollection<Inscricao> ListaInscritos { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task LoadInscritos(int id)
        {
            var t = Task.Run(() => _appServiceEvento.GetByCodigo(id));
            await t;
            ListaInscritos = t.Result.Inscritos.ToList();
            Input.Evento = t.Result;
        }

        public async Task OnGetAsync(int? id)
        {
            if (id != null)
            {
                await LoadInscritos((int)id);
            }
            Input.Data_Inscricao = DateTime.Now;
        }

        public async Task OnPostIncluirPessoaAsync()
        {
            var t = Task.Run(() => _appServicePessoa.ConsultaByCPF(GetCPF));
            await t;
            
            foreach (var p in t.Result)
            {
                Input.Participante = p;
            }

            if (Input.Participante != null)
            {
                var e = Task.Run(() => _appServiceEmpresa.ConsultaByRazaoSocial(new Functions.Mask().Remove(Input.Participante.CPF)));
                await e;

                foreach (var emp in e.Result)
                {
                    Input.Empresa = emp;
                }
            }
        }

        public void OnPostRemoverPessoa()
        {
            Input.Participante = null;
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

        public void OnPostRemoverEmpresa()
        {
            Input.Empresa = null;
        }

        public async Task OnPostIncluirEventoAsync()
        {
            try
            {
                var t = Task.Run(() => _appServiceEvento.GetByCodigo(GetNumeroEvento));
                await t;

                Input.Evento = t.Result;
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        public void OnPostRemoverEvento()
        {
            Input.Evento = null;
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            var ja_inscricao = false;

            if (Input.Evento == null)
            {
                return Page();
            }

            var t = Task.Run(() =>
            {

                //var user = _userManager.Users.FirstOrDefault(m => m.UserName == User.Identity.Name).Id;

                var inscricao = new Inscricao()
                {
                    AplicationUser_Id = User.Identity.Name,
                    Data_Inscricao = DateTime.Now,
                    Presente = false
                };

                var numero = _appServiceInscricao.LastCodigo();

                if (numero < 1)
                    inscricao.Numero = 100001;
                else
                    inscricao.Numero = numero + 1;

                var pessoa = _appServicePessoa.GetById(Input.Participante.Id);

                if (pessoa != null)
                    inscricao.Participante = pessoa;

                if (Input.Empresa != null)
                {
                    var empresa = _appServiceEmpresa.GetById(Input.Empresa.Id);

                    if (empresa != null)
                        inscricao.Empresa = empresa;
                }

                var evento = _appServiceEvento.GetById(Input.Evento.Id);

                if (evento != null)
                    inscricao.Evento = evento;

                ja_inscricao = _appServiceInscricao.JaInscrito(inscricao.Participante.CPF, inscricao.Evento.Codigo);

                if (ja_inscricao)
                {
                    StatusMessage = "Erro: Pessoa já consta na lista de participantes!";
                }
                else
                {
                    _appServiceInscricao.Add(inscricao);
                }
            });

            await t;

            if (ja_inscricao)                
                return Page();
            else
                return RedirectToPage("./Index");
            
        }
    }
}
