using System.Collections.Generic;
using System.Linq;
using InvestControl.Domain.Entity;
using InvestControl.Domain.Repository;
using InvestControl.Infra.Context;

namespace InvestControl.Infra.Repository
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(InvestControlContext context) : base(context)
        {
        }

        public IEnumerable<Evento> ObterTransacoesAteAno(int ano)
        {
            return QueryReadOnly().Where(x => x.Data.Year <= ano);
        }
    }
}