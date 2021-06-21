using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Service
{
    using Interface;
    using Entity;
    public class ServiceRFBSimplesNacional : IServiceSimplesNacional<BaseReceitaFederal>
    {
        public async Task<IEnumerable<BaseReceitaFederal>> ExclusaoSimplesNacional(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.ExclusaoSimplesNacional(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ExclusaoSimplesNacionalMEI(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.ExclusaoSimplesNacionalMEI(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> OptanteMEI(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.OptanteMEI(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> OptanteSimplesNacional(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.OptanteSimplesNacional(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> OptanteSimplesNacionalNaoMEI(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.OptanteSimplesNacionalNaoMEI(s)));

            await t;

            return t.Result;
        }
    }
}
