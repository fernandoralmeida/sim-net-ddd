using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Repository.Cnpj
{
    using Context;
    using Sim.Domain.Cnpj.Entity;
    using Sim.Domain.Cnpj.Interface;
    public class RepositoryJucesp : ICNPJBase<BaseJucesp>
    {
        private readonly JucespContext db;

        public RepositoryJucesp()
        {
            db = new();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<IEnumerable<BaseJucesp>> ListTop10()
        {
            var list = new List<BaseJucesp>();
            var t = Task.Run(() =>
            {

                var top = (from q in db.BaseJucesp.Where(s => s.Municipio == "Jau")
                           orderby q.Data_Situacao_Cadastral descending
                           select q).Take(10);

                list = top.ToList();

            });
            await t;
            return list;
        }

        public async Task<IEnumerable<BaseJucesp>> ListAllAsync()
        {
            var list = new List<BaseJucesp>();
            var t = Task.Run(() =>
            {

                var top = (from q in db.BaseJucesp.Where(s => s.Municipio == "Jau")
                           orderby q.Data_Situacao_Cadastral descending
                           select q);

                list = top.ToList();

            });
            await t;
            return list;
        }

        public async Task<BaseJucesp> GetCnpjAsync(string cnpj)
        {
            var jsp = new BaseJucesp();
            var t = Task.Run(() => db.BaseJucesp.Where(s => s.CNPJ == cnpj && s.Municipio == "Jau"));

            foreach(var reg in await t)
            {
                jsp = reg;
            }

            return jsp;
        }

        public Task<IEnumerable<BaseJucesp>> ListByCnpjAsync(string cnpj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseJucesp>> ListByRazaoSocialAsync(string razaosocial)
        {
            throw new NotImplementedException();
        }
    }
}
