using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{
    using Sim.Cross.Identity;
    public class VMUserRoles : ApplicationUser
    {
        public string RoleId { get; set; }

        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        public List<IdentityRole> ListRoles { get; set; } 

    }
}
