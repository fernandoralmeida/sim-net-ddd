using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository.Shared
{
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.Shared.Interface;
    using Context;
    public class RepositoryContador : RepositoryBase<Contador>, IRepositoryContador
    {
        public RepositoryContador(ApplicationContext dbContext)
            :base(dbContext)
        {   }

        public async Task<string> GetProtocoloAsync(string appuserid, string moduloname)
        {
            var str = string.Empty;
            var t = Task.Run(() =>
            {
                var p = new Contador()
                {
                    Modulo = moduloname,
                    AppUserId = appuserid,
                    Data = DateTime.Now
                };

                _db.Contador.Add(p);
                _db.SaveChanges();

                var protocolo = _db.Contador.Where(s => s.AppUserId == appuserid).OrderBy(c => c.Numero).LastOrDefault();

                str = protocolo.Numero.ToString();
            });

            await t;

           return str;
        }


    }
}
