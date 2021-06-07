using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Entity
{
    public class Pais
    {
        public Pais()
        {

        }
        public Pais(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
        public string Codigo { get; private set; }
        /// <summary>
        /// Nome do País
        /// </summary>
        public string Descricao { get; private set; }
    }
}
