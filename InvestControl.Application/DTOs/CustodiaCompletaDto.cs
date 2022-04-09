namespace InvestControl.Application.DTOs
{
    public class CustodiaCompletaDto
    {
        public string Categoria { get; set; }
        public string CodigoAtivo { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoMedio { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal LucroPrejuizo { get; set; }
    }
}
