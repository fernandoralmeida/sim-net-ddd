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
    using Sim.Domain.Shared.Entity;
    using Sim.Application.Shared.Interface;
    public class StatusModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceStatusAtendimento _appServiceStatusAtendimento;

        public StatusModel(IAppServiceStatusAtendimento appServiceStatusAtendimento)
        {
            _appServiceStatusAtendimento = appServiceStatusAtendimento;
            Input = new();
        }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public StatusAtendimento Input { get; set; }



        private async Task LoadAsync()
        {
            var t = await  _appServiceStatusAtendimento.ListByUser(User.Identity.Name);
            
            if(!t.Any())
            {
                _appServiceStatusAtendimento.Add(new StatusAtendimento() {Id = new Guid(), UnserName = User.Identity.Name, Online = true });
            }

            var t2 = await _appServiceStatusAtendimento.ListByUser(User.Identity.Name);             
            
            foreach (StatusAtendimento sta in t2)
            {               
                Input.Id = sta.Id;
                Input.UnserName = sta.UnserName;
                Input.Online = sta.Online;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var e = _appServiceStatusAtendimento.GetById(id);

            if (e.Online)
                e.Online = false;
            else
                e.Online = true;

            _appServiceStatusAtendimento.Update(e);
            await LoadAsync();
            return Page();
        }
    }
}
