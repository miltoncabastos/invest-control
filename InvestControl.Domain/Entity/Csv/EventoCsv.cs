using System;

namespace InvestControl.Domain.Entity.Csv
{
    public class EventoCsv
    {
        public string CodigoOrigem { get; set; }
        public string CodigoDestino { get; set; }
        public string FatorBase { get; set; }
        public string FatorGanho { get; set; }
        public string Valor { get; set; }
        public string Data { get; set; }
        public string TipoEvento { get; set; }
    }
}