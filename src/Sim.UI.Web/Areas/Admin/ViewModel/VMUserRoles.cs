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

        public IEnumerable<string> ListRoles { get; set; } 

    }
}
