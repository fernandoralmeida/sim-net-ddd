using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Service.CNPJ.WebService
{
    using Entity;

    public interface IReceitaWS
    {
        CNPJ ConsultarCPNJ(string cnpj);
        Task<CNPJ> ConsultarCPNJAsync(string cnpj);
    }
}
