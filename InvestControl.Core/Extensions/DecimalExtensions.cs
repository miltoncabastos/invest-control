using System;
using System.Globalization;

namespace InvestControl.Core.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToMoneyStringPtBr(this decimal value)
        {
            return string.Format("{0:C}", Math.Round(value, 2));
        }
        
        public static string ToStringPtBr(this decimal value)
        {
            return value.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    
        public static decimal Round2Decimals(this decimal valorTotal)
        {
            return Math.Round(valorTotal, 3);
        }
    }
}