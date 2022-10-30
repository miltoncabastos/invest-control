using CsvHelper.Configuration;

namespace InvestControl.Domain.Entity.Csv;

public class MovimentacoesB3CsvMap : ClassMap<MovimentacoesB3Csv>
{
    public MovimentacoesB3CsvMap()
    {
        Map(x => x.EntradaSaida).Name("Entrada/Saída");
        Map(x => x.Data).Name("Data");
        Map(x => x.Movimentacao).Name("Movimentação");
        Map(x => x.Produto).Name("Produto");
        Map(x => x.Instituicao).Name("Instituição");
        Map(x => x.Quantidade).Name("Quantidade");
        Map(x => x.PrecoUnitario).Name("Preço unitário");
        Map(x => x.ValorDaOperacao).Name("Valor da Operação");
    }
}