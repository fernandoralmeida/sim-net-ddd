using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{
    using Sim.Cross.Identity;
    public class VMListUsers
    {
        [DisplayName("Procurar por Id")]
        public string GetUserName { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}
