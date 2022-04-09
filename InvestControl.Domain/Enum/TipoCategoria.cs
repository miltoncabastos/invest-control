using System.ComponentModel;

namespace InvestControl.Domain.Enum
{
    public enum TipoCategoria
    {
        [Description("Fundos imobiliários")]
        FundosImobiliarios,

        [Description("Ações")]
        Acao,

        [Description("BDR")]
        BDR,
        
        [Description("ETF")]
        ETF,
        
        [Description("ETF Exterior")]
        ETFExterior,
        
        [Description("Stocks")]
        Stocks,
    }
}
