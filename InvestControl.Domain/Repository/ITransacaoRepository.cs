using System.Collections;
using System.Collections.Generic;
using InvestControl.Domain.Entity;

namespace InvestControl.Domain.Repository
{
    public interface ITransacaoRepository : IBaseRepository<Transacao>
    {
        public IEnumerable<Transacao> ObterAteAno(int ano);
    }
}