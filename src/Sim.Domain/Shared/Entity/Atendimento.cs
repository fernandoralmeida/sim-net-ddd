using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Shared.Entity
{
    using SDE.Entity;
    public class Atendimento
    {
        public Atendimento()
        {

        }
        public Guid Id { get; set; }
        public int Protocolo { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public Guid Pessoa_Id { get; set; }
        public Guid Empresa_Id { get; set; }
        public string Setor { get; set; }
        public string Canal { get; set; }
        public string Servicos { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public Guid Owner_AppUser_Id { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
