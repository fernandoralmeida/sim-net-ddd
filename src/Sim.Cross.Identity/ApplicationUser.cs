using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sim.Cross.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public const string DisplayName = "DisplayNome";
        public const string DisplayID = "DisplayID";

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public async Task<IdentityResult> AppUserClaims(UserManager<ApplicationUser> userManager)
        {
            var userclaim = await userManager.AddClaimAsync(this, new Claim(DisplayName, Name));
            //userclaim = await userManager.AddClaimAsync(this, new Claim(DisplayID, Id));
            
            return userclaim;
        }
    }
}
