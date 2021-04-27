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
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMRoles Input { get; set; }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => _roleManager.Roles);
            await t;
            Input = new() {
                Roles = t.Result
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole(Input.Name);
                    var roleresult = _roleManager.CreateAsync(role);

                    await roleresult;

                    if (roleresult.IsCompletedSuccessfully)
                    {
                        Input.Roles = _roleManager.Roles;
                        Input.Name = string.Empty;
                        return Page();
                    }
                    return Page();
                }
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role =  _roleManager.FindByIdAsync(id);

                    await role;
                    
                    if (role == null)
                        return Page();

                    IdentityResult identityResult;

                    var delete = _roleManager.DeleteAsync(role.Result);

                    delete.Wait();

                    identityResult = delete.Result;

                    if (!identityResult.Succeeded)
                    {
                        StatusMessage = identityResult.Errors.First().ToString();
                        return Page();
                    }
                    return Page();

                }

                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }
    }
}
