﻿using System;
using System.Collections.Generic;
using System.Linq;

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

    public static class StringExtensions
    {
        public static string Mask(this string value, string mask, char substituteChar = '#')
        {
            int valueIndex = 0;
            try
            {
                return new string(mask.Select(maskChar => maskChar == substituteChar ? value[valueIndex++] : maskChar).ToArray());
            }
            catch (IndexOutOfRangeException e)
            {
                throw new Exception("Valor muito curto para substituir todos os caracteres substitutos na máscara", e);
            }
        }

        public static string MaskRemove(this string valor)
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
