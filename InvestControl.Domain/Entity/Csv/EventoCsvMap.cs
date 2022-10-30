using CsvHelper.Configuration;

namespace InvestControl.Domain.Entity.Csv;

public class EventoCsvMap : ClassMap<EventoCsv>
{
    public EventoCsvMap()
    {
        Map(x => x.CodigoOrigem).Name("codigo-origem");
        Map(x => x.CodigoDestino).Name("codigo-destino");
        Map(x => x.FatorBase).Name("fator-base");
        Map(x => x.FatorGanho).Name("fator-ganho");
        Map(x => x.Valor).Name("valor");
        Map(x => x.Data).Name("data");
        Map(x => x.TipoEvento).Name("tipo-evento");
    }
}