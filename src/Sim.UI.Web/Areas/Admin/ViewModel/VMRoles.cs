using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{
    public class VMRoles
    {
        public VMRoles() { Id = new Guid(); }

        public Guid Id { get; set; }

        [DisplayName("Role Name")]
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string StatusMessage { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

    }
}
