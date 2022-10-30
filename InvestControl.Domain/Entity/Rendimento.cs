using System;
using InvestControl.Domain.Enum;

namespace InvestControl.Domain.Entity;

public class Rendimento : BaseEntity
{
    public string CodigoAtivo { get; set; }
    public TipoCategoria TipoCategoria { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
}