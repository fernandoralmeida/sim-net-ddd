using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Domain.Cnpj.Entity
{
    public class Simples
    {
        public Simples()
        {

        }
        public Simples(string cnpj, string opcsimples, string dataopcsimples,
            string dataexcsimples, string opcmei, string dataopcmei, string dataexcmei)
        {
            CNPJBase = cnpj;
            OpcaoSimples = opcsimples;
            DataOpcaoSimples = dataopcsimples;
            DataExclusaoSimples = dataexcsimples;
            OpcaoMEI = opcmei;
            DataOpcaoMEI = dataopcmei;
            DataExclusaoMEI = dataexcmei;
        }
        public string CNPJBase { get; private set; }

        private string _opcsimples = string.Empty;
        /// <summary>
        /// Sim, Não
        /// </summary>
        public string OpcaoSimples
        {
            get { return GetSimples(_opcsimples); }
            private set { _opcsimples = value; }
        }
        public string DataOpcaoSimples { get; private set; }
        public string DataExclusaoSimples { get; private set; }

        private string _opcmei = string.Empty;
        /// <summary>
        /// Sim, Não
        /// </summary>
        public string OpcaoMEI {
            get { return GetMEI(_opcmei); }
            private set { _opcmei = value; }
        }
        public string DataOpcaoMEI { get; private set; }
        public string DataExclusaoMEI { get; private set; }

        private string GetSimples(string valor)
        {
            if (valor == "S")
                return "Simples Nacional";

            else
                return "";
        }

        private string GetMEI(string valor)
        {
            if (valor == "S")
                return "MEI";

            else
                return "";
        }
    }
}
