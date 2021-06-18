using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Service
{
    using Entity;
    using Cnpj.Interface;
    public class ServiceRFB : IServiceCnpj<BaseReceitaFederal>
    {
        public async Task<IEnumerable<BaseReceitaFederal>> EmpresasAtivas(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.EmpresaAtiva(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> EmpresasBaixadas(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.EmpresaBaixada(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> EmpresasInaptas(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.EmpresaInapta(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> EmpresasNulas(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.EmpresaNula(s)));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> EmpresasSuspensas(IEnumerable<BaseReceitaFederal> obj)
        {
            var t = Task.Run(() => obj.Where(s => s.EmpresaSuspensa(s)));

            await t;

            return t.Result;
        }
    }
}
