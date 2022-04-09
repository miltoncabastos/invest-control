using InvestControl.Domain.Enum;
using System;

namespace InvestControl.Domain.Entity
{
    public class Transacao : BaseEntity
    {
        public DateTime DataOperacao { get; set; }
        public TipoCategoria TipoCategoria { get; set; }
        public string CodigoAtivo { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int CorretoraId { get; set; }
        public Corretora Corretora { get; set; }
    }
}
