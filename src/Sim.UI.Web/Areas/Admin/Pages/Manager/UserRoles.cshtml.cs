using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{
    using ViewModel;
    using Sim.Cross.Identity;


    public class UserRolesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public UserRolesModel(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMUserRoles Input { get; set; }

        [Required]
        [BindProperty]   
        public string Selecionado { get; set; }

        public SelectList RoleList { get; set; }

        private async Task LoadAsync(string id)
        {
            var roles = _roleManager.Roles.ToList();
            RoleList = new SelectList(roles, nameof(IdentityRole.Name));            


            var u = _userManager.FindByIdAsync(id);
            await u;

            var r = _userManager.GetRolesAsync(u.Result);
            await r;

            Input = new()
            {
                Id = u.Result.Id,
                UserName = u.Result.UserName,
                Name = u.Result.Name,
                LastName = u.Result.LastName,
                Gender = u.Result.Gender,
                Email = u.Result.Email,
                ListRoles = r.Result
            };
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await LoadAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAddRoleAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            await _userManager.AddToRoleAsync(user, Selecionado);

            return RedirectToPage("./UserRoles", new { id });
        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(string id, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.RemoveFromRoleAsync(user, role);

                return RedirectToPage("./UserRoles", new { id = user.Id });
            }
            catch
            {
                return Page();
            }
        }
    }
}
