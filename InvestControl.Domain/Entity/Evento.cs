using System;
using InvestControl.Domain.Entity.Enums;

namespace InvestControl.Domain.Entity
{
    public class Evento : BaseEntity
    {
        public string CodigoOrigem { get; set; }
        public string CodigoDestino { get; set; }
        public int FatorBase { get; set; }
        public int FatorGanho { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoEvento TipoEvento { get; set; }
    }
}