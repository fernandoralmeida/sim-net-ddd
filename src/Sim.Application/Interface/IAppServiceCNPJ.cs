using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Application.Interface
{
    public interface IAppServiceCNPJ<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAsync();
        Task<TEntity> GetByIdAsync(int id);
        void Dispose();
        Task<IEnumerable<TEntity>> Listar();
        Task<IEnumerable<TEntity>> ListarPorMunicipio(string municipio);
        Task<IEnumerable<TEntity>> ListarPorEndereco(string endereco, string municipio);
        Task<IEnumerable<TEntity>> ListarPorAtividade(string atividade, string municipio);
        Task<IEnumerable<TEntity>> ListarPorSituacaoCadastral(string situacaocadastral, string municipio);
        Task<IEnumerable<TEntity>> ListarPorEnderecoAtividade(string endereco, string atividade, string municipio);
        Task<IEnumerable<TEntity>> ListarPorBairroAtividade(string bairro, string atividade, string municipio);
        Task<IEnumerable<TEntity>> ListarPorBairro(string bairro, string municipio);
    }
}
