using System;

namespace InvestControl.Infra.Extensions
{
    public static class DecimalExtensions
    {
        private static decimal Round2Decimals(this decimal value)
        {
            return Math.Round(value, 3);
        }
    }
}