using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

        private async Task LoadAsync(string id)
        {
            var roles = _roleManager.Roles.ToList();

            Input = new();

            Input.ListRoles = roles;

            var u = _userManager.FindByIdAsync(id);
            u.Wait();

            var r = _userManager.GetRolesAsync(u.Result);
            await r;

            if (r.Result.Count > 0)
            {
                var nr = _roleManager.Roles.AsQueryable();
                nr = nr.Where(c => c.Name.Contains(r.Result[0]));

                Input.ListRoles = nr.ToList();
            }

            Input.Id = u.Result.Id;
            Input.UserName = u.Result.UserName;
            Input.Name = u.Result.Name;
            Input.LastName = u.Result.LastName;
            Input.Gender = u.Result.Gender;
            Input.Email = u.Result.Email;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await LoadAsync(id);
            return Page();
        }

        public async Task<IActionResult> AddUserRoles(string x, string y)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(x);

                await _userManager.AddToRoleAsync(user, y);

                return Page();
            }
            catch
            {
                return Page();
            }
        }

        public async Task<IActionResult> RemoveUserRoles(string x, string y)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(x);

                await _userManager.RemoveFromRoleAsync(user, y);

                return Page();
            }
            catch
            {
                return Page();
            }
        }
    }
}
