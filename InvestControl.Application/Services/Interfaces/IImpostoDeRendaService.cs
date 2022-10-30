using InvestControl.Application.DTOs;
using System.Collections.Generic;

namespace InvestControl.Application.Services.Interfaces
{
    public interface IImpostoDeRendaService
    {
        IList<CustodiaDto> CalcularCustodiaAnual(int ano);
        IList<LucroPrejuizoDto> CalcularLucroOuPrejuizoMensal(int ano);
        IList<ImpostoMensalDto> CalcularImpostoAPagarMensal(int ano);
    }
}
