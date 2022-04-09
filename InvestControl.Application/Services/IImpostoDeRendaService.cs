using InvestControl.Application.DTOs;
using System.Collections.Generic;

namespace InvestControl.Application.Services
{
    public interface IImpostoDeRendaService
    {
        IList<CustodiaDto> CalcularCustodiaAnual(int ano);
        IList<LucroPrejuizoDto> CalcularLucroEPrejuizoMensal(int ano);
    }
}
