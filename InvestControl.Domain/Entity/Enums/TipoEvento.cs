using System.ComponentModel;

namespace InvestControl.Domain.Entity.Enums
{
    public enum TipoEvento
    {
        [Description("BONIFICACAO")]
        Bonificacao,
        [Description("DESMEMBRAMENTO")]
        Desmembramento,
        [Description("AGRUPAMENTO")]
        Agrupamento,
        [Description("CONVERSAO")]
        Conversao
    }
}