using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Identity
{
    public class RepositoryUser : IAppServiceUser
    {
        protected IdentityContext db;
        public RepositoryUser(IdentityContext identity)
        {
            db = identity;
        }
        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.AppUsers.ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return db.AppUsers.Find(id);
        }

        public void Unlock(string id)
        {
            db.AppUsers.Find(id).LockoutEnabled = false;
            db.SaveChanges();
        }
    }
}
