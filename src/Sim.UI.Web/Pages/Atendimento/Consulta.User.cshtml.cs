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
    using Sim.Cross.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Authorize(Roles = "Administrador")]
    public class ConsultaUserModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceUser _appIdentity;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public SelectList ListaAtendentes { get; set; }
        public class InputModel
        {
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string UserName { get; set; }

            public string CNPJ { get; set; }

            public ICollection<Atendimento> ListaAtendimento { get; set; }
        }

        public ConsultaUserModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceUser appServiceUser)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            Input = new();
        }

        private async Task LoadUsers()
        {
            var t = Task.Run(()=> _appIdentity.GetAll());
            await t;

            if (t != null)
            {
                ListaAtendentes = new SelectList(t.Result, nameof(ApplicationUser.UserName), nameof(ApplicationUser.UserName), null);
            }
        }

        public async Task OnGetAsync()
        {
            await LoadUsers();
            //var lista = Task.Run(() => _appServiceAtendimento.ListAll());
            //await lista;
            //Input.ListaAtendimento = lista.Result.ToList();
            Input.DataI = new DateTime(DateTime.Now.Year, 1, 1);
            Input.DataF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Input.ListaAtendimento = new List<Atendimento>();
        }

        public async Task OnPostAsync()
        {
            try
            {
                await LoadUsers();

                //var dataI = Input.DataI.Value.Date;
                var dataF = Input.DataF;

                var lista = Task.Run(() => _appServiceAtendimento.GetByUserNamePeriodo(Input.UserName, dataF)); ;
                await lista;
                Input.ListaAtendimento = lista.Result.ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Atendimento>();
            }
        }
    }
}
