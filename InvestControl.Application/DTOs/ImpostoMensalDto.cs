namespace InvestControl.Application.DTOs;

public class ImpostoMensalDto
{
    public int Mês { get; set; }
    public string Categoria { get; set; }
    public decimal Total { get; set; }
    public decimal Percentual { get; set; }
    public decimal ImpostoAPagar { get; set; }
}