using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Domain.Shared.Entity;
    using Sim.Application.Shared.Interface;
    using Sim.Cross.Identity;
    using Functions;

    [Authorize]
    public class ConsultaModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IAppServiceUser _appIdentity;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public ParamModel GetParam { get; set; }

        public SelectList ListaAtendentes { get; set; }

        public SelectList ListaServicos { get; set; }

        public class InputModel
        {
            [DisplayName("Data Inicial")]
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DisplayName("Data Final")]
            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string CPF { get; set; }

            public string CNPJ { get; set; }

            public string Nome { get; set; }

            [DisplayName("Razão Social")]
            public string RazaSocial { get; set; }

            public string CNAE { get; set; }

            public string Servico { get; set; }

            public string Atendente { get; set; }

            public ICollection<Atendimento> ListaAtendimento { get; set; }
        }

        public class ParamModel
        {
            public string param1 { get; set; }
            public string param2 { get; set; }
            public string param3 { get; set; }
            public string param4 { get; set; }
            public string param5 { get; set; }
            public string param6 { get; set; }
            public string param7 { get; set; }
            public string param8 { get; set; }
            public string param9 { get; set; }
        }

        public ConsultaModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceUser appServiceUser,
            IAppServiceServico appServiceServico)
        {            
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            _appServiceServico = appServiceServico;
            Input = new();
            GetParam = new();
        }

        public void OnGet()
        {
            //var lista = Task.Run(() => _appServiceAtendimento.ListAll());
            //await lista;
            //Input.ListaAtendimento = lista.Result.ToList();
            Input.DataI = new DateTime(DateTime.Now.Year, 1, 1);
            Input.DataF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Input.ListaAtendimento = new List<Atendimento>();
            LoadUsers().Wait();
            LoadServicos().Wait();
        }

        private async Task LoadUsers()
        {
            var t = Task.Run(() => _appIdentity.GetAll());
            await t;

            if (t != null)
            {
                ListaAtendentes = new SelectList(t.Result, nameof(ApplicationUser.UserName), nameof(ApplicationUser.UserName), null);
            }
        }

        private async Task LoadServicos()
        {
            var t = Task.Run(() => _appServiceServico.List());
            await t;

            if (t != null)
            {
                ListaServicos = new SelectList(t.Result, nameof(Servico.Nome), nameof(Servico.Nome), null);
            }
        }

        public async Task OnPostListByDataAsync()
        {
            try
            {
                var param = new List<object>() {
                    Input.DataI.Value.Date,
                    Input.DataF.Value.Date,
                    Input.CPF,
                    Input.Nome,
                    Input.CNPJ,
                    Input.RazaSocial,
                    Input.CNAE,
                    Input.Servico,
                    Input.Atendente  };

                var lista = await _appServiceAtendimento.ListByParam(param);
                
                Input.ListaAtendimento = lista.ToList();

                string d1 = Input.DataI.Value.Date.ToShortDateString();
                string d2 = Input.DataF.Value.Date.ToShortDateString();

                GetParam.param1 = d1.MaskRemove();
                GetParam.param2 = d2.MaskRemove();
                GetParam.param3 = Input.CPF != null ? Input.CPF.MaskRemove() : "0";
                GetParam.param4 = Input.Nome != null ? Input.Nome : "0";
                GetParam.param5 = Input.CNPJ != null ? Input.CNPJ.MaskRemove() : "0";
                GetParam.param6 = Input.RazaSocial != null ? Input.RazaSocial : "0";
                GetParam.param7 = Input.CNAE != null ? Input.CNAE.MaskRemove() : "0";
                GetParam.param8 = Input.Servico != null ? Input.Servico : "0";
                GetParam.param9 = Input.Atendente != null ? Input.Atendente : "0";

            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Atendimento>();
            }

            await LoadServicos();
            await LoadUsers();
        }

        public async Task OnPostAtPendentesAsync()
        {
            try
            {
                var lista = Task.Run(() => _appServiceAtendimento.AtendimentoAtivo(User.Identity.Name));
                await lista;
                Input.ListaAtendimento = lista.Result.ToList();
                await LoadServicos();
                await LoadUsers();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Atendimento>();
            }
        }

        public async Task OnPostAtCanceladosAsync()
        {
            try
            {
                var lista = Task.Run(() => _appServiceAtendimento.AtendimentosCancelados(User.Identity.Name));
                await lista;
                Input.ListaAtendimento = lista.Result.ToList();
                await LoadServicos();
                await LoadUsers();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Atendimento>();
            }

        }

        public async Task OnPostListByPessoaAsync()
        {
            var lista = Task.Run(() => _appServiceAtendimento.GetByPessoa(Input.CPF));
            await lista;
            Input.ListaAtendimento = lista.Result.ToList();
        }
        public async Task OnPostListByEmpresaAsync()
        {
            var lista = Task.Run(() => _appServiceAtendimento.GetByEmpresa(Input.CNPJ));
            await lista;
            Input.ListaAtendimento = lista.Result.ToList();
        }
    }
}
