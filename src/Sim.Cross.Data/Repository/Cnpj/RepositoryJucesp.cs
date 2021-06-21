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

        public RepositoryJucesp(JucespContext context)
        {
            db = context;
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
                           orderby q.Data_Situacao_Cadastral ascending
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

        public async Task<IEnumerable<BaseJucesp>> ListByCnpjAsync(string cnpj)
        {
            var t = Task.Run(() => db.BaseJucesp.Where(s => s.CNPJ.Contains(cnpj)).ToList());
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<BaseJucesp>> ListByRazaoSocialAsync(string razaosocial, string muinicipio)
        {
            var t = Task.Run(() => db.BaseJucesp.Where(s => s.Nome_Empresarial.Contains(razaosocial) && s.Municipio.Contains(muinicipio)).ToList());
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<BaseJucesp>> ListByLogradouroAsync(string logradouro, string muinicipio)
        {
            var t = Task.Run(() => db.BaseJucesp.Where(s => s.Nome_Logradouro.Contains(logradouro) && s.Municipio.Contains(muinicipio)).ToList());
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<BaseJucesp>> ListByBairroAsync(string bairro, string muinicipio)
        {
            var t = Task.Run(() => db.BaseJucesp.Where(s => s.Bairro.Contains(bairro) && s.Municipio.Contains(muinicipio)).ToList());
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<BaseJucesp>> ListByAtividadeAsync(string atividade, string muinicipio)
        {
            var t = Task.Run(() => db.BaseJucesp.Where(s => s.Atividade_Economica.Contains(atividade) && s.Municipio.Contains(muinicipio)).ToList());
            await t;
            return t.Result;
        }

        public Task<IEnumerable<BaseJucesp>> ListBySociosAsync(string atividade, string municipio)
        {
            return null;
        }

        public Task<IEnumerable<BaseJucesp>> ListAllOptanteSimplesAsync(string municipio)
        {
            return null;
        }
    }
}
