using System.ComponentModel;

namespace InvestControl.Domain.Enum
{
    public enum TipoOperacao
    {
        [Description("C")]
        Compra = 10,
        
        [Description("V")]
        Venda = 20,
    }
}
