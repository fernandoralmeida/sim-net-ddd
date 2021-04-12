using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Identity
{
    public interface IAppServiceUser : IDisposable
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();
        void Unlock(string id);
    }
}
