using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Domain.Shared.Entity;
    using Sim.Application.Shared.Interface;

    [Authorize]
    public class ConsultaModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string CPF { get; set; }

            public string CNPJ { get; set; }

            public ICollection<Atendimento> ListaAtendimento { get; set; }
        }

        public ConsultaModel(IAppServiceAtendimento appServiceAtendimento)
        {            
            _appServiceAtendimento = appServiceAtendimento;
            Input = new();
        }

        public void OnGet()
        {
            //var lista = Task.Run(() => _appServiceAtendimento.ListAll());
            //await lista;
            //Input.ListaAtendimento = lista.Result.ToList();
            Input.DataI = new DateTime(DateTime.Now.Year, 1, 1);
            Input.DataF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public async Task OnPostListByDataAsync()
        {
            var lista = Task.Run(() => _appServiceAtendimento.ListByPeriodo(Input.DataI.Value.Date, Input.DataF.Value.Date));
            await lista;
            Input.ListaAtendimento = lista.Result.ToList();
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
