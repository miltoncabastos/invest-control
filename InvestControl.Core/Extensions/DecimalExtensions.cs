using System.Globalization;

namespace InvestControl.Core.Extensions;

public static class DecimalExtensions
{
    public static string ToStringPtBr(this decimal value)
    {
        return value.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
    }
    
    public static decimal Round2Decimals(this decimal valorTotal)
    {
        return Math.Round(valorTotal, 3);
    }
}