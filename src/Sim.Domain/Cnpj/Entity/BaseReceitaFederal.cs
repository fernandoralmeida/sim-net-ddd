using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Entity
{
    public class BaseReceitaFederal
    {
        public BaseReceitaFederal()
        {

        }
        public BaseReceitaFederal(int id, string cnpj, Empresa empresa,
            Estabelecimento estabelecimento, ICollection<Socio> socios,
            Simples simplesnacional, CNAE atividadeprincipal,
            ICollection<CNAESecundaria> atividadesecundarias,
            NaturezaJuridica naturezaJuridica,
            MotivoSituacaoCadastral motivoSituacaoCadastral,
            Municipio municipio,
            QualificacaoSocio qualificacaoSocio)
        {
            Id = id;
            CNPJ = cnpj;
            Empresa = empresa;
            Estabelecimento = estabelecimento;
            Socios = socios;
            SimplesNacional = simplesnacional;
            AtividadePrincipal = atividadeprincipal;
            AtividadeSecundarias = atividadesecundarias;
            NaturezaJuridica = naturezaJuridica;
            MotivoSituacaoCadastral = motivoSituacaoCadastral;
            Cidade = municipio;
            QualificacaoSocio = qualificacaoSocio;
        }
        public int Id { get; private set; }
        public string CNPJ { get; private set; }
        public virtual Empresa Empresa { get; private set; }
        public virtual Estabelecimento Estabelecimento { get; private set; }
        public virtual ICollection<Socio> Socios { get; private set; }
        public virtual Simples SimplesNacional { get; private set; }
        public virtual CNAE AtividadePrincipal { get; private set; }
        public virtual ICollection<CNAESecundaria> AtividadeSecundarias { get; private set; }
        public virtual NaturezaJuridica NaturezaJuridica { get; private set; }
        public virtual MotivoSituacaoCadastral MotivoSituacaoCadastral { get; private set; }
        public virtual Municipio Cidade { get; private set; }
        public virtual QualificacaoSocio QualificacaoSocio { get; private set; }

        public bool EmpresaNula(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral.Equals("01");
        }

        public bool EmpresaAtiva(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral.Equals("02");
        }

        public bool EmpresaSuspensa(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral.Equals("03"); 
        }

        public bool EmpresaInapta(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral.Equals("04"); 
        }
        
        public bool EmpresaBaixada(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral.Equals("08"); 
        }

    }
}
