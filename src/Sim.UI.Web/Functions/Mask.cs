using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sim.UI.Web.Functions
{
    public class Mask
    {
        public string Remove(string valor)
        {
            try
            {
                var str = valor;
                str = new string((from c in str
                                  where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                                  select c
                       ).ToArray());

                return str;
            }
            catch
            {
                return valor;
            }
        }
    }
}
