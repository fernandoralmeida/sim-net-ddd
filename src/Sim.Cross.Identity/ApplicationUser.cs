using Microsoft.AspNetCore.Identity;

namespace Sim.Cross.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}
