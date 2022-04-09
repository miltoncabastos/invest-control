using System.Collections.Generic;

namespace InvestControl.Application.DTOs
{
    public class LucroPrejuizoDto
    {
        public string Categoria { get; set; }
        public int Mes { get; set; }
        public decimal Total { get; set; }
        public IList<LucroPrejuizoUnidadeDto> Ativos { get; set; } = new List<LucroPrejuizoUnidadeDto>();
    }
}
