using System.Collections.Generic;
using System.Linq;
using InvestControl.Domain.Entity;
using InvestControl.Domain.Repository;
using InvestControl.Infra.Context;

namespace InvestControl.Infra.Repository
{
    public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepository(InvestControlContext context) : base(context)
        {
        }

        public IEnumerable<Transacao> ObterAteAno(int ano)
        {
            return QueryReadOnly().Where(t => t.DataOperacao.Year <= ano);
        }
    }
}