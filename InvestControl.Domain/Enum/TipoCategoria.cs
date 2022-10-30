using System.ComponentModel;

namespace InvestControl.Domain.Enum
{
    public enum TipoCategoria
    {
        [Description("Não definido")]
        NaoDefinido,
        
        [Description("Fundos imobiliários")]
        FundosImobiliarios,

        [Description("Ações")]
        Acao,

        [Description("BDR")]
        Bdr,
        
        [Description("ETF")]
        Etf,
        
        [Description("ETF Exterior")]
        EtfExterior,
        
        [Description("Stocks")]
        Stocks,
        
        [Description("Tesouro direto")]
        TesouroDireto
    }
}
