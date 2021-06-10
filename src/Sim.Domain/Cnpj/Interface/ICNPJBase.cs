using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Interface
{
    public interface ICNPJBase<TEntity> where TEntity : class
    {
        void Dispose();
        Task<IEnumerable<TEntity>> ListTop10();
        Task<IEnumerable<TEntity>> ListAllAsync();
        Task<TEntity> GetCnpjAsync(string cnpj);
        Task<IEnumerable<TEntity>> ListByCnpjAsync(string cnpj);
        Task<IEnumerable<TEntity>> ListByRazaoSocialAsync(string razaosocial, string municipio);
        Task<IEnumerable<TEntity>> ListByLogradouroAsync(string logradouro, string municipio);
        Task<IEnumerable<TEntity>> ListByBairroAsync(string bairro, string municipio);
        Task<IEnumerable<TEntity>> ListByAtividadeAsync(string atividade, string municipio);
        Task<IEnumerable<TEntity>> ListBySociosAsync(string atividade, string municipio);
    }
}
