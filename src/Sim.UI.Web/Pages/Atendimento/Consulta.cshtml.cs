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
    using System.IO;
    using OfficeOpenXml;

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

        public IActionResult OnPostExport()
        {
            var stream = new MemoryStream();
            var t = Task.Run(() =>
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

                var list = new List<ExportModel>();
                var cont = 1;
                foreach (var e in _appServiceAtendimento.ListByParam(param).Result)
                {
                    if (e.Empresa != null)
                        list.Add(new ExportModel
                        {
                            N = cont++,
                            Data = e.Data.Value.ToString("MMMyyyy"),
                            Cliente = e.Pessoa.Nome,
                            Empresa = e.Empresa.CNPJ,
                            Atividade = e.Empresa.Atividade_Principal,
                            Contato = e.Pessoa.Tel_Movel,
                            Servico = e.Servicos,
                            Descricao = e.Descricao,
                            Setor = e.Setor
                        });
                    else
                        list.Add(new ExportModel
                        {
                            N = cont++,
                            Data = e.Data.Value.ToString("MMMyyyy"),
                            Cliente = e.Pessoa.Nome,
                            Empresa = "",
                            Atividade = "",
                            Contato = e.Pessoa.Tel_Movel,
                            Servico = e.Servicos,
                            Descricao = e.Descricao,
                            Setor = e.Setor
                        });
                }
                return list;
            });

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var epackage = new ExcelPackage(stream);
            var worksheet = epackage.Workbook.Worksheets.Add("Lista");
            worksheet.Cells.LoadFromCollection(t.Result, true);
            epackage.SaveAsync();

            stream.Position = 0;
            string excelname = $"lista-atend-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
        }

        public async Task OnPostViewAsync()
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
    }
}
