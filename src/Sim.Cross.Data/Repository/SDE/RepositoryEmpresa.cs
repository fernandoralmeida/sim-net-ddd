using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository.SDE
{
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.SDE.Interface;
    using Context;
   

    public class RepositoryEmpresa : RepositoryBase<Empresa>, IRepositoryEmpresa
    {
        
        public RepositoryEmpresa(ApplicationContext dbContext)
            :base(dbContext)
        {
            
        }

        public IEnumerable<Empresa> ConsultaByCNAE(string cnae)
        {
            return _db.Empresa.Where(p => p.CNAE_Principal == cnae);
        }

        public IEnumerable<Empresa> ConsultaByCNPJ(string cnpj)
        {
            return _db.Empresa.Where(p => p.CNPJ == cnpj);
        }

        public IEnumerable<Empresa> ConsultaByRazaoSocial(string name)
        {
            return _db.Empresa.Where(p => p.Nome_Empresarial.Contains(name) || p.Nome_Fantasia.Contains(name));
        }

        public IEnumerable<Empresa> ListEmpresasQsa(Guid id)
        {

            return _db.Empresa.Where(p => p.Id == id);
        }

        public async Task<IEnumerable<Empresa>> UltimasFormalizacoes()
        {
            return await Task.Run(() => _db.Empresa.Where(p => p.Data_Abertura >= DateTime.Now.AddDays(-60)).OrderByDescending(o => o.Data_Abertura));
        }
    }
}
