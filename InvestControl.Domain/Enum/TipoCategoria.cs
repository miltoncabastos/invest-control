using System.ComponentModel;
using InvestControl.Domain.Attributes;

namespace InvestControl.Domain.Enum
{
    public enum TipoCategoria
    {
        [Description("Não Definido")]
        [CsvName("Não definido")]
        NaoDefinido,
        
        [Description("Fundos Imobiliários")]
        [CsvName("Fundos imobiliários")]
        FundosImobiliarios,

        [Description("Ações")]
        [CsvName("Ações")]
        Acao,

        [Description("BDR")]
        [CsvName("BDR")]
        Bdr,
        
        [Description("ETF")]
        [CsvName("ETF")]
        Etf,
        
        [Description("ETF Exterior")]
        [CsvName("ETF Exterior")]
        EtfExterior,
        
        [Description("Stocks")]
        [CsvName("Stocks")]
        Stocks,
        
        [Description("Tesouro Direto")]
        [CsvName("Tesouro direto")]
        TesouroDireto
    }
}
