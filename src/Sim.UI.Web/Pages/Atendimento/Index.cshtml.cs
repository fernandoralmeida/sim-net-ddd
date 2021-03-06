using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Atendimento
{
    using Sim.Cross.Identity;
    using Sim.Domain.Shared.Entity;
    using Sim.Application.Shared.Interface;

    [Authorize]
    public class IndexModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
            //_userManager = userManager;
            Input = new();
            Input.DataAtendimento = DateTime.Now.Date;
        }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
       
        public class InputModel
        {
            [DisplayName("Data")]
            [DataType(DataType.Date)]
            public DateTime? DataAtendimento { get; set; }

            public ICollection<Atendimento> ListaAtendimento { get; set; }
        }

        private async Task LoadAsync(DateTime? date)
        {
            Input.DataAtendimento = date;
            //var user = await _userManager.GetUserAsync(User);

            var t = Task.Run(() => _appServiceAtendimento.MeusAtendimentos(User.Identity.Name, date));
            await t;
            Input.ListaAtendimento = t.Result.ToList();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await LoadAsync(Input.DataAtendimento);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await LoadAsync(Input.DataAtendimento);

                if (Input.ListaAtendimento.Count == 0)
                {
                    StatusMessage = string.Format("Erro: N?o h? atendimentos para {0}", Input.DataAtendimento.Value.Date);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Atendimento>();
            }

            return Page();
        }

        public JsonResult OnGetPreview(string id)
        {
            var list = new List<Atendimento>();

            var atendimemnto_ativo = Task.Run(() => _appServiceAtendimento.GetAtendimento(new Guid(id)));

            atendimemnto_ativo.Wait();

            list.Add(atendimemnto_ativo.Result);

            return new JsonResult(list);
        }
    }
}
